using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    public static GameData gameData;
    string path;
    [SerializeField] UserInfo currentUserInfo;

    private void Awake()
    {
        if (gameData == null)
        {
            gameData = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (gameData != this)
            {
                Destroy(this.gameObject);
            }
        }
        path = Application.persistentDataPath + "/SaveGame.txt";
    }

    private void Start()
    {
        CargarDesdeArchivo();
    }

    public void CargarDesdeArchivo()
    {
        
        if (File.Exists(path))
        {
            Cargar();
        }
        else
        {
            currentUserInfo = new UserInfo();
            Guardar();
        }
    }

    public UserInfo GetUserInfo()
    {
        return currentUserInfo;
    }

    public void Guardar()
    {
        File.WriteAllText(path, JsonUtility.ToJson(currentUserInfo));
    }

    public void Cargar()
    {
        currentUserInfo = JsonUtility.FromJson<UserInfo>(File.ReadAllText(path));
    }
}
