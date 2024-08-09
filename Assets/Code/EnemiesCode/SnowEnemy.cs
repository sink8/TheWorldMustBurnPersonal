using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEnemy : MonoBehaviour
{
    [SerializeField] GameObject waterEnemyPrefab;
    public int snowHealth = 1;
    bool canChange = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") )
        {
            snowHealth -= 1;
            AudioFW.Play("HitEnemySnow");

            if (canChange && snowHealth <= 0)
            {
                // instantiate höyry olio
                Instantiate(waterEnemyPrefab, gameObject.transform.position, transform.rotation);
                
            }

            Destroy(gameObject, 0.3f);
        }
    }
}
