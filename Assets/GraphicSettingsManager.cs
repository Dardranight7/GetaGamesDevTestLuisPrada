using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicSettingsManager : MonoBehaviour
{
    [SerializeField] GameObject postProcess;
    // Start is called before the first frame update
    void Start()
    {
        AplicarConfiguraciones();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
    }

    public void AplicarConfiguraciones()
    {
        UserInfo currentUserInfo = GameData.gameData.GetUserInfo();
        postProcess.SetActive(currentUserInfo.enablePostProces);
    }
}
