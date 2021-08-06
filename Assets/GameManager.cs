using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] UIManager uiManager;
    [SerializeField] float tiempoJuego;
    float tiempoInicio;

    // Start is called before the first frame update
    void Start()
    {
        tiempoInicio = Time.time + tiempoJuego;
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.SetValorUI((tiempoInicio - Time.time).ToString("F0") + "seg");
        if (tiempoInicio - Time.time < 0)
        {
            Debug.Log("fin del juego");
        }
    }
}
