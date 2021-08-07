using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DardranightCortinilla : MonoBehaviour
{
    public static DardranightCortinilla DardraCortinilla;
    [SerializeField] CanvasGroup panel;
    [Header("Transicion")]
    [Tooltip("El tipo de efecto esperado.")]
    [SerializeField] efecto efectoTransicion;
    [Range(0.01f,2)]
    [SerializeField] float tiempoTransicion;
    [Tooltip("Texto para mostrar porcentaje de carga, opcional.")]
    [SerializeField] TextMeshProUGUI textoPorcentaje; 
    AsyncOperation operacionCargaEscena;
    bool ocupado = false;
    bool cargandoEscena = false;


    enum efecto
    {
        fadeinout
    }

    private void Awake()
    {
        if (DardraCortinilla == null)
        {
            DardraCortinilla = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (DardraCortinilla == this)
            {
                Debug.Log("Cargada Escena Nueva");
            }
            else
            {
                DardranightCortinilla.DardraCortinilla.Imprimir("Ya existe una instancia");
                Destroy(gameObject);
            }
        }
    }

    public bool ComprobarFinal()
    {
        return !ocupado;
    }

    public void Imprimir(string msg)
    {
        Debug.Log(msg + " Desde instancia original -> " + (DardraCortinilla == this));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CargarEscena(int numeroEscena)
    {
        if (cargandoEscena)
        {
            return;
        }
        switch (efectoTransicion)
        {
            case efecto.fadeinout:
                    CargaFade(numeroEscena);
                break;
            default:
                break;
        }
    }

    async void CargaFade(int numeroEscena)
    {
        cargandoEscena = true;
        await FadeIn();
        operacionCargaEscena = SceneManager.LoadSceneAsync(numeroEscena);
        while (!operacionCargaEscena.isDone)
        {
            if (textoPorcentaje != null)
            {
                textoPorcentaje.text = operacionCargaEscena.progress.ToString("000 %");
            }
            await Task.Yield();
        }
        await FadeOut();
        cargandoEscena = false;
    }

    async Task<bool> FadeIn()
    {
        ocupado = true;
        panel.alpha = 0;
        while (panel.alpha != 1)
        {
            panel.alpha = Mathf.MoveTowards(panel.alpha, 1, tiempoTransicion * Time.deltaTime);
            await Task.Yield();
        }
        ocupado = false;
        return true;
        //await Task.Delay(System.TimeSpan.FromSeconds(1));
    }

    async Task<bool> FadeOut()
    {
        ocupado = true;
        panel.alpha = 1;
        while (panel.alpha != 0)
        {
            panel.alpha = Mathf.MoveTowards(panel.alpha, 0, tiempoTransicion * Time.deltaTime);
            await Task.Yield();
        }
        ocupado = false;
        return true;
    }
}
