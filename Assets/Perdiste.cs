using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perdiste : MonoBehaviour
{
    [SerializeField] GameObject textoCualquierTecla;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(RutinaPerdiste());
    }

    IEnumerator RutinaPerdiste()
    {
        yield return new WaitForSeconds(3);
        textoCualquierTecla.SetActive(true);
        GameData.gameData.GetUserInfo().partidasPerdidas += 1;
        GameData.gameData.Guardar();
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DardranightCortinilla.DardraCortinilla.CargarEscena(1);
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    DardranightCortinilla.DardraCortinilla.CargarEscena(0);
                    break;
                }
            }
            yield return null;
        }
    }
}
