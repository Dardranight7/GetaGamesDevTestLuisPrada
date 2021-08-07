using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject target;
    [SerializeField] string functionName = "CambiarSombrero";
    [SerializeField] int data;

    public void OnPointerClick(PointerEventData eventData)
    {
        target.SendMessage(functionName, data);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
