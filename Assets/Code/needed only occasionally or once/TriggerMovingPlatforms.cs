using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovingPlatforms : MonoBehaviour
{
    [SerializeField] PlatformController platformController;
    [SerializeField] PlatformControllerVertical platformControllerVert;

    [SerializeField] bool vertical = false;
    [SerializeField] bool horizontal = false;


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sparks"))
        {
            if(horizontal) { platformController.enabled = true;
            } 
            else { platformController.enabled = false;
                platformControllerVert.enabled = true;
            }
        }
    }
}
