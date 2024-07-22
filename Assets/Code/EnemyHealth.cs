using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1f;
    [SerializeField] bool canChange = true;
    [SerializeField] GameObject piviHahmo;

    [SerializeField] ParticleSystem cryDrops;
    [SerializeField] GameObject deathZone;

    [SerializeField] float cryCycle = 10;
    [SerializeField] float timer = 0;
    [SerializeField] float crytime = 4;
    [SerializeField] bool cryBool = true;
    [SerializeField] bool CancryBool = true;

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

    private void Update()
    {
        if(CancryBool)
        {
            timer += Time.deltaTime;
            if (!cryBool && timer >= cryCycle)
            {
                StartCoroutine(PlayCry());
            }
        }

    }

    IEnumerator PlayCry()
    {
        cryBool = true;
        cryDrops.Play();
        AudioFW.PlayLoop("Rain");
        yield return new WaitForSeconds(crytime);

        AudioFW.StopLoop("Rain");
        cryDrops.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        timer = 0;
        cryBool = false;

    }

}
