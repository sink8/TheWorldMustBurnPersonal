using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KokkoEndStanding : MonoBehaviour
{
    public Animator anim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.Play("kokkoseisoo");

        }
    }
}
