using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileArc : MonoBehaviour
{
    private Vector3 target;
    private float speed;
    private float arcHeight;
    public bool isLeaf = true;
    Animator anim;
    private bool hasLanded = false;
    private Transform player;

    private bool isExploding = false;
    public float impactRadius = 0.5f;
    public ParticleSystem part;
    public void Launch(Vector3 target, float speed, float arcHeight)
    {
        this.target = target;
        this.speed = speed;
        this.arcHeight = arcHeight;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(ArcMove());

    }

    private IEnumerator ArcMove()
    {
        Vector3 startPos = transform.position;
        float distance = Vector2.Distance(startPos, target);
        float travelTime = distance / speed;
        float time = 0;

        while (time < travelTime && !isExploding)
        {
            float t = time / travelTime;
            Vector3 currentPos = Vector3.Lerp(startPos, target, t);
            currentPos.y += arcHeight * 4 * (t - t * t);

            transform.position = currentPos;

            // Check if close enough to player
            if (player != null && Vector2.Distance(transform.position, player.position) < impactRadius)
            {
                StartCoroutine(ExplodeThenDestroy());
                yield break;
            }

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        if (!isExploding)
        {
            StartCoroutine(ExplodeThenDestroy());
        }
    }

        private IEnumerator ExplodeThenDestroy()
    {
        isExploding = true;

        if (anim != null)
        {
            if (isLeaf)
            {
                anim.SetTrigger("lehtiprojectile_destoyed");
                hasLanded = true;

                // Wait for animation to finish (assume it's 0.5 seconds)
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                part.Play();
                AudioFW.Play("HitEnemyWater");
                yield return new WaitForSeconds(0.5f);
            }
        }

        Destroy(gameObject);
    }
}
