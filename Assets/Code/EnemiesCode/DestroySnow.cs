using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnow : MonoBehaviour
{
    public float health = 2f;
    [SerializeField] bool canChange = true;

    [SerializeField] GameObject vesihahmo;
    bool isDead = false;

    GameObject allFires;
    void Start()
    {
        allFires = GameObject.Find("AllFires");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        HandleImpact(collision.gameObject);

            //if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player")) {
            //    health -= 1;
            //    AudioFW.Play("HitEnemyWater");
            //    if (canChange && health <= 0)
            //    {
            //        // instantiate h�yry olio
            //        var vesi = Instantiate(vesihahmo, gameObject.transform.position, transform.rotation);
            //        vesi.transform.SetParent(allFires.transform);
            //        // if (cloudMoves)
            //        // {
            //        //     vesi.GetComponentInChildren<MoveBetweenTwoPoints>().shouldMove = true;
            //        // }
                
            //    }

            //    Destroy(gameObject);
            //}
        

        }

    private void HandleImpact(GameObject hitObject) {
        // 1. Check if already dying to prevent multiple spawns
        if (isDead) return;

        if (hitObject.CompareTag("Sparks") || hitObject.CompareTag("Player")) {
            health -= 1;
            AudioFW.Play("HitEnemyWater");

            if (health <= 0) {
                isDead = true; // Mark as dead immediately

                if (canChange && vesihahmo != null) {
                    var vesi = Instantiate(vesihahmo, transform.position, transform.rotation);

                    if (allFires != null)
                        vesi.transform.SetParent(allFires.transform);
                }

                // Destroy the snow object after the delay
                Destroy(gameObject, 0.3f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        //if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player")) {
        //    health -= 1;
        //    AudioFW.Play("HitEnemyWater");
        //    if (canChange && health <= 0)
        //    {
        //        // instantiate h�yry olio
        //        var vesi = Instantiate(vesihahmo, gameObject.transform.position, transform.rotation);
        //        vesi.transform.SetParent(allFires.transform);
        //        // if (cloudMoves)
        //        // {
        //        //     vesi.GetComponentInChildren<MoveBetweenTwoPoints>().shouldMove = true;
        //        // }

        //    }

        //    Destroy(gameObject);
        //}
        HandleImpact(collision.gameObject);

    }




}
