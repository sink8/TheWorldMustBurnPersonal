using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatforms : MonoBehaviour
{
    [SerializeField] PlatformController platformController;
    [SerializeField] PlatformControllerVertical platformControllerVert;
    [SerializeField] MoveBetweenTwoPoints movet;

    [SerializeField] bool vertical = false;
    [SerializeField] bool horizontal = false;
    [SerializeField] bool points = false;
    Animator animator;
    [SerializeField] GameObject particle;

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

            GameObject smokeParticle = Instantiate(particle);

            smokeParticle.transform.position = gameObject.transform.position;
            //smokeParticle.transform.SetParent(allFires.transform);
            Destroy(smokeParticle, 2);

            animator.Play("kristalli_pun");
            if(vertical) { platformController.enabled = true;
            } 
            
            if(horizontal)
            { platformController.enabled = false;
                platformControllerVert.enabled = true;
            }

            if(points)
            {
                movet.enabled = true;
                movet.shouldMove = true;
            }
        }
    }
}
