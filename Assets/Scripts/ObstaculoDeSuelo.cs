using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoDeSuelo : MonoBehaviour
{
    [SerializeField] float valor = 10;
    [SerializeField] List<GameObject> aparienciaVisual = new List<GameObject>();
    [SerializeField] int tipo = 0;

    private void Start()
    {
        CambiarApariencia();
    }

    public void CambiarApariencia()
    {
        for (int i = 0; i < aparienciaVisual.Count; i++)
        {
            aparienciaVisual[i].SetActive(false);
            if (i == tipo)
            {
                aparienciaVisual[i].SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (tipo)
            {
                case 0:
                        other.GetComponent<PlayerController>().AceleracionInstantanea(transform.forward, valor);
                    break;
                case 1:
                        other.GetComponent<PlayerController>().PoderSalto(valor);
                    break;
                case 2:
                        FindObjectOfType<GameManager>().AumentarTiempo((int)valor);
                        gameObject.SetActive(false);
                    break;
                case 3:
                        other.GetComponent<PlayerController>().Banana();
                        gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }    
    }
}
