using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victoria : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject mensajeCualquierTecla;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI textoTiempo;

    void OnEnable()
    {
        StartCoroutine(rutinaVictoria());
    }

    IEnumerator rutinaVictoria()
    {
        textoTiempo.text = (Time.time - gameManager.GetTiempoInicial()).ToString("F3") + "s";
        yield return new WaitForSeconds(3);
        mensajeCualquierTecla.SetActive(true);
        UserInfo currenUserInfo = GameData.gameData.GetUserInfo();
        currenUserInfo.partidasGanadas += 1;
        if ((Time.time - gameManager.GetTiempoInicial()) < currenUserInfo.mejorTiempo)
        {
            currenUserInfo.mejorTiempo = (Time.time - gameManager.GetTiempoInicial());
        }
        GameData.gameData.Guardar();
        while (true)
        {
            if (Input.anyKeyDown)
            {
                DardranightCortinilla.DardraCortinilla.CargarEscena(0);
                break;
            }
            yield return null;
        }
    }
}
