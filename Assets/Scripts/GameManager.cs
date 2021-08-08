using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] UIManager uiManager;
    [SerializeField] float tiempoJuego;
    [SerializeField] GameObject VentanaVictoria;
    [SerializeField] GameObject VentanaPerdiste;
    bool victoria;
    bool perdiste;
    float tiempoTotal;
    float tiempoIncial;

    // Start is called before the first frame update
    void Start()
    {
        tiempoTotal = Time.time + tiempoJuego;
        tiempoIncial = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (victoria)
        {
            return;
        }
        if (perdiste)
        {
            return;
        }
        uiManager.SetValorUI((tiempoTotal - Time.time).ToString("00:00"));
        if (tiempoTotal - Time.time < 0)
        {
            VentanaPerdiste.SetActive(true);
            perdiste = true;
        }
    }

    public void SetVictoria()
    {
        if (perdiste)
        {
            return;
        }
        victoria = true;
        VentanaVictoria.SetActive(true);
    }

    public bool getFinJuego()
    {
        if (victoria || perdiste)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetTiempoInicial()
    {
        return tiempoIncial;
    }

    public void AumentarTiempo(int tiempoExtra = 10)
    {
        tiempoTotal += tiempoExtra;
    }
}
