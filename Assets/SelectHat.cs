using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHat : MonoBehaviour
{
    [SerializeField] List<GameObject> sombreros = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        UserInfo userInfo = GameData.gameData.GetUserInfo();
        sombreros[userInfo.indiceSombrero].SetActive(true);
    }

    public void CambiarSombrero(int indice)
    {
        for (int i = 0; i < sombreros.Count; i++)
        {
            sombreros[i].SetActive(false);
            if (i == indice)
            {
                sombreros[i].SetActive(true);
            }
        }
    }

    public List<GameObject> GetSombreros()
    {
        return sombreros;
    }
}
