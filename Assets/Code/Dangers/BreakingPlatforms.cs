using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatforms : MonoBehaviour
{

    [SerializeField] float timeTillBreakingStarts = 1f;
    [SerializeField] float dissolveDuration = 1f;
    [SerializeField] float timeTillRefromsBack = 4f;

    [SerializeField] float animTime, respawnTime = 2f;

    public float timerBreaking = 0f;
    public float dissolveTimer = 0f;
    public float reformTimer = 0f;
    


    private Renderer platformRenderer;
    private Collider2D platformCollider;
    private Material platformMaterial;
    Animator anim;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider2D>();
        platformMaterial = platformRenderer.material;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
 
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //playerOnPlatform = true;
            StartCoroutine("Crumble");
        }
    }

    IEnumerator Crumble()
    {
        anim.Play("BreakingPlatformBreak");
        yield return new WaitForSeconds(animTime);
        Components(false);
        yield return new WaitForSeconds(respawnTime);
        anim.Play("BreakingPlatformRespawn");
        Components(true);
    }

    private void Components(bool state)
    {
        platformRenderer.enabled = state;
        platformCollider.enabled = state;
    }


}
