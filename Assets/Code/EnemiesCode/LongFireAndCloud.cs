using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongFireAndCloud : MonoBehaviour
{
    public List<Animator> animList;

   
    [SerializeField] bool activateByHand = false;

    [SerializeField] MoveBetweenTwoPoints moveBetweenTwo;
    void Start()
    {
        
    }

    void StartAnimations()
    {
        for (int i = 0; i < animList.Count; i++)
        {
            animList[i].Play("FireUpNewAnimation");
        }

        moveBetweenTwo.shouldMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sparks"))
        {
            StartAnimations();
        }
    }
}
