using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LehtiOlio : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject LehtiBurned;
    [SerializeField] GameObject dialogue;
    [SerializeField] ParticleSystem ash;
    [SerializeField] float delayBeforeBurned = 2f; // Delay in seconds
    EnemyAttack enemyAttack;
    SpriteRenderer sprite;

    private bool hasBurned = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        enemyAttack = GetComponent<EnemyAttack>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

            if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player"))
            {
            hasBurned = true;   
            animator.Play("LEHTIOLIO_BURN");
            //AudioFW.Play("HitEnemyWater");

            //ash.Play();
            //dialogue.SetActive(false);
            enemyAttack.StopShooting();
            StartCoroutine(SwitchToBurned());

        }

    }

    private void Update() {
        if(enemyAttack.partrolling && hasBurned == false) {
            animator.Play("Lehti_walk_New_1");
        }
    }
    IEnumerator SwitchToBurned()
    {
        enemyAttack.enabled = false;
        yield return new WaitForSeconds(delayBeforeBurned);
        LehtiBurned.SetActive(true);
        dialogue.SetActive(false);
        sprite.enabled = false;

    }
}
