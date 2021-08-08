using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField, Range(0f, 100f)]
	float velocidadMaxima = 10f;

	[SerializeField, Range(0f, 100f)]
	float aceleracionMaxima = 10f, aceleracionMaximaEnAire = 1f;

	[SerializeField, Range(0f, 10f)]
	float alturaSalto = 2f;

	[SerializeField, Range(0, 5)]
	int limiteSaltos = 0;

	[SerializeField, Range(0, 90)]
	float anguloMaximoSuelo = 25f;

	[SerializeField] GameObject carrito;

	[SerializeField] GameObject llantaIzquierda, llantaDerecha;

	[SerializeField] GameObject visualCarrito ,direccionDerecha, direccionIzquierda;

	[SerializeField] AudioSource audioSource;

	[SerializeField] GameManager gameManager;

	Quaternion rotacionFrente;

	[SerializeField] GameObject targetRotation;

	Rigidbody rigidBody;

	Vector3 velocidad, velocidadDeseada;

	Vector3 contactoNormal;

	bool saltando;

	bool controlPerdido = false;

	int conteoToquesSuelo;

	bool enSuelo => conteoToquesSuelo > 0;

	int faseSalto;

	float minProductoPuntoSuelo;

	void OnValidate()
	{
		minProductoPuntoSuelo = Mathf.Cos(anguloMaximoSuelo * Mathf.Deg2Rad);
	}

	void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		OnValidate();
	}

    private void Start()
    {
		rotacionFrente = visualCarrito.transform.localRotation;
    }

    void Update()
	{
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        if (controlPerdido)
        {
			playerInput = Vector3.zero;
        }
        if (gameManager.getFinJuego())
        {
			playerInput = Vector3.zero;
        }
		velocidadDeseada = transform.forward * playerInput.y * velocidadMaxima;
		audioSource.volume = Mathf.Log10(reglaDeTres(velocidadMaxima, 0.6f, rigidBody.velocity.sqrMagnitude));
		saltando |= Input.GetButtonDown("Jump");
	}

	float reglaDeTres(float a,float b,float c)
    {
		return (b * c)/a;
    }

	void FixedUpdate()
	{
		ActualizarEstado();
		AjustarVelocidad();

		if (saltando)
		{
			saltando = false;
			Salto();
		}

		rigidBody.velocity = velocidad;
		MoverRotacion();
		LimpiarEstado();
	}

	public void AceleracionInstantanea(Vector3 direccion, float potencia)
    {
		rigidBody.AddForce(direccion * potencia * 100, ForceMode.Impulse);
    }

	public void PoderSalto(float poder = 200)
    {
		rigidBody.AddForce(transform.forward + transform.up * poder, ForceMode.Impulse);
    }

	public void Banana()
    {
		AceleracionInstantanea(transform.forward, 1);
		StartCoroutine(PerderControl());
	}

	IEnumerator PerderControl()
    {
		controlPerdido = true;
		yield return new WaitForSeconds(1.8f);
		controlPerdido = false;
    }

	void MoverRotacion()
	{
		if (controlPerdido)
		{
			visualCarrito.transform.Rotate(new Vector3(0, 360 * Time.fixedDeltaTime, 0));
			return;
		}
		rigidBody.MoveRotation(Quaternion.RotateTowards(rigidBody.rotation, targetRotation.transform.rotation, 120 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal")));
		Quaternion rotacionSobreSuelo = Quaternion.RotateTowards(carrito.transform.rotation,Quaternion.LookRotation(ProyectarEnPlanoDeContacto(transform.forward)),240*Time.fixedDeltaTime);
		carrito.transform.rotation = rotacionSobreSuelo;
		llantaIzquierda.transform.localRotation = Quaternion.RotateTowards(llantaIzquierda.transform.localRotation, Quaternion.FromToRotation(transform.forward, Vector3.Lerp(transform.forward, transform.right * Input.GetAxisRaw("Horizontal"), 0.25f)), 180 * Time.fixedDeltaTime);
		llantaDerecha.transform.localRotation = Quaternion.RotateTowards(llantaDerecha.transform.localRotation, Quaternion.FromToRotation(transform.forward, Vector3.Lerp(transform.forward, transform.right * Input.GetAxisRaw("Horizontal"), 0.25f)), 180 * Time.fixedDeltaTime);
		visualCarrito.transform.localRotation = Quaternion.RotateTowards(visualCarrito.transform.localRotation, Quaternion.FromToRotation(transform.forward, Direcciones(Input.GetAxisRaw("Horizontal"))),120 * Time.fixedDeltaTime);
	}

	Vector3 Direcciones(float a)
    {
        if (a > 0)
        {
			return direccionDerecha.transform.forward;
        }
		else if (a < 0)
        {
			return direccionIzquierda.transform.forward;
        }
        else
        {
			return transform.forward;
        }
    }

	void LimpiarEstado()
	{
		conteoToquesSuelo = 0;
		contactoNormal = Vector3.zero;
	}

	void ActualizarEstado()
	{
		velocidad = rigidBody.velocity;
		if (enSuelo)
		{
			faseSalto = 0;
			if (conteoToquesSuelo > 1)
			{
				contactoNormal.Normalize();
			}
		}
		else
		{
			contactoNormal = Vector3.up;
		}
	}

	void AjustarVelocidad()
	{
		Vector3 xAxis = ProyectarEnPlanoDeContacto(Vector3.right).normalized;
		Vector3 zAxis = ProyectarEnPlanoDeContacto(Vector3.forward).normalized;

		float valorX = Vector3.Dot(velocidad, xAxis);
		float valorY = Vector3.Dot(velocidad, zAxis);

		float acceleration = enSuelo ? aceleracionMaxima : aceleracionMaximaEnAire;
		float maxSpeedChange = acceleration * Time.deltaTime;

		float newX =
			Mathf.MoveTowards(valorX, velocidadDeseada.x, maxSpeedChange);
		float newZ =
			Mathf.MoveTowards(valorY, velocidadDeseada.z, maxSpeedChange);

		velocidad += xAxis * (newX - valorX) + zAxis * (newZ - valorY);
	}

	void Salto()
	{
		if (enSuelo || faseSalto < limiteSaltos)
		{
			faseSalto += 1;
			float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * alturaSalto);
			float alignedSpeed = Vector3.Dot(velocidad, contactoNormal);
			if (alignedSpeed > 0f)
			{
				jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
			}
			velocidad += contactoNormal * jumpSpeed;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void OnCollisionStay(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void EvaluateCollision(Collision collision)
	{
		for (int i = 0; i < collision.contactCount; i++)
		{
			Vector3 normal = collision.GetContact(i).normal;
			if (normal.y >= minProductoPuntoSuelo)
			{
				conteoToquesSuelo += 1;
				contactoNormal += normal;
			}
		}
	}

	Vector3 ProyectarEnPlanoDeContacto(Vector3 vector)
	{
		return vector - contactoNormal * Vector3.Dot(vector, contactoNormal);
	}
}