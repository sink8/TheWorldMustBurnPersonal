using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilvi_behavior : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("pilvi_1_test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
