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

	[SerializeField] GameObject targetRotation;

	Rigidbody rigidBody;

	Vector3 velocidad, velocidadDeseada;

	Vector3 contactoNormal;

	bool saltando;

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

	void Update()
	{
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		velocidadDeseada = transform.forward * playerInput.y * velocidadMaxima;
		
		saltando |= Input.GetButtonDown("Jump");
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

	public void PoderSalto()
    {
		rigidBody.AddForce(transform.forward + transform.up * 500, ForceMode.Impulse);
    }

	void MoverRotacion()
    {
		rigidBody.MoveRotation(Quaternion.RotateTowards(rigidBody.rotation, targetRotation.transform.rotation, 120 * Time.fixedDeltaTime * Input.GetAxisRaw("Horizontal")));
		carrito.transform.localRotation = Quaternion.RotateTowards(carrito.transform.localRotation, Quaternion.FromToRotation(ProyectarEnPlanoDeContacto(transform.forward), Vector3.Lerp(ProyectarEnPlanoDeContacto(transform.forward), ProyectarEnPlanoDeContacto(transform.right) * Input.GetAxisRaw("Horizontal"), 0.25f)), 120 * Time.fixedDeltaTime);
		llantaIzquierda.transform.localRotation = Quaternion.RotateTowards(carrito.transform.localRotation, Quaternion.FromToRotation(transform.forward, Vector3.Lerp(transform.forward, transform.right * Input.GetAxisRaw("Horizontal"), 0.25f)), 180 * Time.fixedDeltaTime);
		llantaDerecha.transform.localRotation = Quaternion.RotateTowards(carrito.transform.localRotation, Quaternion.FromToRotation(transform.forward, Vector3.Lerp(transform.forward, transform.right * Input.GetAxisRaw("Horizontal"), 0.25f)), 180 * Time.fixedDeltaTime);
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