using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseStart : MonoBehaviour
{
    
    public FuseBurning fuseBurn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fuseBurn.Ignite();
        }
    }
}
