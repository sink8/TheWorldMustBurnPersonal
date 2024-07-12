using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] bool canChange = true;
    [SerializeField] GameObject piviHahmo;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player")) {
            health = 0;
            if(canChange)
            {
                // instantiate höyry olio
                Instantiate(piviHahmo, gameObject.transform.position, transform.rotation);
            }

            Destroy(gameObject, 0.3f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Palo")) {
            Destroy(gameObject);
        }
    }

}
