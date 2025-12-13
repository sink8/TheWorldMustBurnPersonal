using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnow : MonoBehaviour
{
    public float health = 2f;
    [SerializeField] bool canChange = true;

    [SerializeField] GameObject vesihahmo;


    GameObject allFires;
    void Start()
    {
        allFires = GameObject.Find("AllFires");
    }

    private void OnCollisionEnter2D(Collision2D collision) {

            if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player")) {
                health -= 1;
                AudioFW.Play("HitEnemyWater");
                if (canChange && health <= 0)
                {
                    // instantiate h�yry olio
                    var vesi = Instantiate(vesihahmo, gameObject.transform.position, transform.rotation);
                    vesi.transform.SetParent(allFires.transform);
                    // if (cloudMoves)
                    // {
                    //     vesi.GetComponentInChildren<MoveBetweenTwoPoints>().shouldMove = true;
                    // }
                
                }

                Destroy(gameObject, 0.3f);
            }
        

        }


    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player")) {
                health -= 1;
                AudioFW.Play("HitEnemyWater");
                if (canChange && health <= 0)
                {
                    // instantiate h�yry olio
                    var vesi = Instantiate(vesihahmo, gameObject.transform.position, transform.rotation);
                    vesi.transform.SetParent(allFires.transform);
                    // if (cloudMoves)
                    // {
                    //     vesi.GetComponentInChildren<MoveBetweenTwoPoints>().shouldMove = true;
                    // }
                
                }

                Destroy(gameObject, 0.3f);
            }
        
    }

    private void Update()
    {


    }


}
