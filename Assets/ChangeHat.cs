using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHat : MonoBehaviour
{
    [SerializeField] SelectHat selectHat;
    int indiceSombrero;

    private void Start()
    {
        indiceSombrero = GameData.gameData.GetUserInfo().indiceSombrero;
    }

    public void CambiarSombrero(int a)
    {
        if (a > 0)
        {
            indiceSombrero = indiceSombrero + 1 >= selectHat.GetSombreros().Count ? 0 : indiceSombrero + 1;
            selectHat.CambiarSombrero(indiceSombrero);
        }
        else
        {
            indiceSombrero = indiceSombrero - 1 < 0 ? selectHat.GetSombreros().Count - 1 : indiceSombrero - 1;
            selectHat.CambiarSombrero(indiceSombrero);
        }
        GameData.gameData.GetUserInfo().indiceSombrero = indiceSombrero;
    }
}
