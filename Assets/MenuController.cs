using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> botones;
    [SerializeField] Color Seleccionado, Deseleccionado;
    [SerializeField] int indiceBotones;

    [SerializeField] GameObject canvasCaptarDatos;
    [SerializeField] TextMeshProUGUI valorUsuario;
    // Start is called before the first frame update
    void Start()
    {
        CambiarApariencia();
        Saludar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            indiceBotones = indiceBotones + 1 >= botones.Count ? 0 : indiceBotones + 1;
            CambiarApariencia();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            indiceBotones = indiceBotones - 1 < 0 ? botones.Count - 1 : indiceBotones - 1;
            CambiarApariencia();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (indiceBotones == 0)
            {
                DardranightCortinilla.DardraCortinilla.CargarEscena(1);
            }
            else if (indiceBotones == 1)
            {
                Application.Quit();
            }
        }
    }

    public void Saludar()
    {
        string nombreUsuario = PlayerPrefs.GetString("nombre", "");
        if (nombreUsuario == "")
        {
            valorUsuario.text = "Invitado";
            canvasCaptarDatos.gameObject.SetActive(true);
        }
        else
        {
            valorUsuario.text = nombreUsuario;
        }
    }

    public void SetNombreUsuario(string _nombre)
    {
        PlayerPrefs.SetString("nombre", _nombre);
        canvasCaptarDatos.gameObject.SetActive(false);
        Saludar();
    }

    public void CambiarApariencia()
    {
        for (int i = 0; i < botones.Count; i++)
        {
            botones[i].color = Deseleccionado;
            if (i == indiceBotones)
            {
                botones[i].color = Seleccionado;
            }
        }
    }
}
