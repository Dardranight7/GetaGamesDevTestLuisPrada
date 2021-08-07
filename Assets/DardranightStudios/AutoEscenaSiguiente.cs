using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEscenaSiguiente : MonoBehaviour
{
    [Tooltip("Si es verdadero comienza a contar el tiempo apenas se instancia")]
    [SerializeField] bool playOnStart = true;
    [Tooltip("Tiempo en segundos")]
    [SerializeField] float tiempo;
    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
        {
            StartCoroutine(CargarAlTiempo());
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CargarAlTiempo()
    {
        yield return new WaitForSeconds(tiempo);
        DardranightCortinilla.DardraCortinilla.CargarEscena(0);
    }
}
