using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenaSiguiente : MonoBehaviour
{
    [SerializeField] AudioClip efectoSonido;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<KeyCode> tecla;
    [SerializeField] int escenaACargar;
    bool activo = false;
   
    // Update is called once per frame
    void Update()
    {
        bool pressed = false;
        for (int i = 0; i < tecla.Count; i++)
        {
            if (Input.GetKeyDown(tecla[i]))
            {
                pressed = true;
                break;
            }
        }
        if (pressed && !activo && DardranightCortinilla.DardraCortinilla.ComprobarFinal())
        {
            Debug.Log("Holiwis");
            if (audioSource != null && efectoSonido != null)
            {
                audioSource.PlayOneShot(efectoSonido);
            }
            DardranightCortinilla.DardraCortinilla.CargarEscena(escenaACargar);
            activo = true;
        }
    }
}
