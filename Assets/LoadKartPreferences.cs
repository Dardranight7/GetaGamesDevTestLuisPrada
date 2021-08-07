using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadKartPreferences : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer chasis, personaje;
    // Start is called before the first frame update
    void Start()
    {
        UserInfo userInfo = GameData.gameData.GetUserInfo();
        chasis.material.color = userInfo.colorChasis;
        personaje.material.color = userInfo.colorPersonaje;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
