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
    SpriteRenderer sprite;

    private bool hasBurned = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

            if (collision.gameObject.CompareTag("Sparks") || collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Player"))
            {
            animator.Play("LEHTIOLIO_BURN");
            //AudioFW.Play("HitEnemyWater");

            ash.Play();
        dialogue.SetActive(false);
            StartCoroutine(SwitchToBurned());

        }
        

    }
    IEnumerator SwitchToBurned()
    {
        yield return new WaitForSeconds(delayBeforeBurned);
        LehtiBurned.SetActive(true);
        sprite.enabled = false;
    }
}
