using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoDeSuelo : MonoBehaviour
{
    [SerializeField] float potencia = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().AceleracionInstantanea(transform.forward,potencia);
            other.GetComponent<PlayerController>().PoderSalto();
        }    
    }
}
