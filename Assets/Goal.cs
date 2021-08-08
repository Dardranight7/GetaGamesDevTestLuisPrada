using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool isGoald = false;
    public bool isPassed = false;
    [SerializeField] List<Goal> goals = new List<Goal>();
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        isPassed = true;
        if (!isGoald)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < goals.Count; i++)
            {
                if (!goals[i].isPassed)
                {
                    return;
                }
            }
            gameManager.SetVictoria();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isGoald)
        {
            goals = new List<Goal>(FindObjectsOfType<Goal>());
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
