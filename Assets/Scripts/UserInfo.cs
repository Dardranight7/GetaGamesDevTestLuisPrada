using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInfo
{
    [SerializeField] public string nombre = "";
    // Car Customize Data
    [SerializeField] public Color colorChasis = new Color(0.9803922f, 0.1921569f, 0.2156863f,1f), colorPersonaje = new Color(0.3411765f, 0.7019608f, 0.8000001f,1f);
    [SerializeField] public int indiceSombrero = 0;
    // Settings 
    [SerializeField] public bool enablePostProces = true;
}
