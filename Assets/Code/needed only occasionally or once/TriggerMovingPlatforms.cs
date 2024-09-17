using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatforms : MonoBehaviour
{
    [SerializeField] PlatformController platformController;
    [SerializeField] PlatformControllerVertical platformControllerVert;

    [SerializeField] bool vertical = false;
    [SerializeField] bool horizontal = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sparks"))
        {
            animator.Play("kristalli_pun");
            if(vertical) { platformController.enabled = true;
            } 
            else { platformController.enabled = false;
                platformControllerVert.enabled = true;
            }
        }
    }
}
