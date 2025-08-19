using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lopputrigger : MonoBehaviour
{
    StoreScores scores;
    public bool loppuNRO_1 = true;
    public GameObject loppu1;
    public GameObject loppu2;

    void Start()
    {
        scores = FindObjectOfType<StoreScores>();
    }

    
    void Update()
    {
        
    }

    public void kumpiLoppu()
    {
        if (scores.countForTheEnd > 15)
        {
            loppuNRO_1 = false;
        }
        else loppuNRO_1 = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            kumpiLoppu();

            if(loppuNRO_1 == true)
            {
                loppu1.SetActive(true);
            }
            else
            {
                loppu2.SetActive(true);
            }
        }

     }
}
