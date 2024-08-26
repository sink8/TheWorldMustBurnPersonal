using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWater : MonoBehaviour
{

    [SerializeField] ParticleSystem drops;
    [SerializeField] ParticleSystem fallingWater;
    [SerializeField] GameObject vesi_1;
    [SerializeField] GameObject vesi_2;
    [SerializeField] GameObject block;
    [SerializeField] Animator animator_1;
    [SerializeField] Animator animator_fire;

    [SerializeField] Collider2D col;
    [SerializeField] Collider2D col2;
    [SerializeField] float time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Palo"))
        {
            col2.enabled = true;
            AudioFW.Play("HitEnemyWater");
            fallingWater.Play();
            
            //animator_1.Play("WaterLevelDown");
            animator_1.Play("wawywater2");
            animator_fire.Play("FireUpNewAnimation");
            
            StartCoroutine(later());
            //Destroy(gameObject, 0.7f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Palo"))
        {
            AudioFW.Play("HitEnemyWater");
            fallingWater.Play();

            //animator_1.Play("WaterLevelDown");
            animator_1.Play("wawywater2");
            animator_fire.Play("FireUpNewAnimation");
            col2.enabled = true;
            StartCoroutine(later());
            //Destroy(gameObject, 0.7f);
        }
    }

    IEnumerator later()
    {

        yield return new WaitForSeconds(3);

        col.enabled = false;
        animator_fire.enabled = false;
        //block.SetActive(false);
        fallingWater.Stop();
    }

}
