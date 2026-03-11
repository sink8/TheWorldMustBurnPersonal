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
    private bool willAttack = false;
    private float initialDialogueScaleX;
    void Start()
    {
        initialDialogueScaleX = Mathf.Abs(dialogue.transform.localScale.x);
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        enemyAttack = GetComponent<EnemyAttack>();
        if (enemyAttack != null) {
            // Only runs if the component actually exists
            
            if (transform.localScale.x < 0) {
                enemyAttack.isFacingRight = true;
            } else {
                enemyAttack.isFacingRight = false;
            }
        }
    }

    void ApplyDialogueScale() {

        float finalScaleX;

        if (enemyAttack.isFacingRight == true) {
            // If facing right, force negative scale
            finalScaleX = -initialDialogueScaleX;
        } else {
            // If facing left (natural), force positive scale
            finalScaleX = initialDialogueScaleX;
        }

        dialogue.transform.localScale = new Vector3(
            finalScaleX,
            dialogue.transform.localScale.y,
            dialogue.transform.localScale.z
        );
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
        if (enemyAttack != null)
        {
            if (enemyAttack.partrolling && hasBurned == false) {
                animator.Play("Lehti_walk_New_1");
                
            }

        }
        if (willAttack)
        {
            animator.Play("Lehti_idle_New_2");
        }

        if (enemyAttack != null) {
            ApplyDialogueScale();
        }
    }
    IEnumerator SwitchToBurned()
    {
        enemyAttack.enabled = false;
        dialogue.SetActive(false);
        yield return new WaitForSeconds(delayBeforeBurned);
        LehtiBurned.SetActive(true);
        sprite.enabled = false;

    }
}
