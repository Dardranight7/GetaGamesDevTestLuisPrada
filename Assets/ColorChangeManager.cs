using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeManager : MonoBehaviour
{
    [SerializeField] ColorPickerUnityUI colorPicker;
    [SerializeField] GameObject target;
    Material targetMaterial;
    Color colorPickeado;
    // Start is called before the first frame update
    void Start()
    {
        targetMaterial = target.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        if (colorPicker.value != colorPickeado)
        {
            CambiarColor();
        }
    }

    public Color GetColorPickeado()
    {
        return colorPickeado;
    }

    public void CambiarColor()
    {
        targetMaterial.color = colorPicker.value;
        colorPickeado = colorPicker.value;
    }
}
