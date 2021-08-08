using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rankin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI valorTiempo, valorGanadas, valorPerdidas, valorTotal;
    // Start is called before the first frame update
    void Start()
    {
        UserInfo userInfo = GameData.gameData.GetUserInfo();
        if (userInfo.mejorTiempo == 1000)
        {
            valorTiempo.text = "0";
        }
        else
        {
            valorTiempo.text = userInfo.mejorTiempo.ToString("F3");
        }
        valorGanadas.text = userInfo.partidasGanadas.ToString();
        valorPerdidas.text = userInfo.partidasPerdidas.ToString();
        valorTotal.text = (userInfo.partidasGanadas + userInfo.partidasPerdidas).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
