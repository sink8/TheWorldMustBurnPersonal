using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilvi_behavior : MonoBehaviour
{
    Animator animator;
    public bool cloudCanMove;
    MoveBetweenTwoPoints moveBetween;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("pilvi_1_test");
        moveBetween = GetComponent<MoveBetweenTwoPoints>();
        if(cloudCanMove == true)
        {
            moveBetween.shouldMove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
