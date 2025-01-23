using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 2f;
    [SerializeField] bool canChange = true;
    [SerializeField] bool cloudMoves = false;
    [SerializeField] GameObject piviHahmo;

    [SerializeField] ParticleSystem cryDrops;
    [SerializeField] GameObject deathZone;

    [SerializeField] float cryCycle = 10;
    [SerializeField] float timer = 0;
    [SerializeField] float crytime = 4;
    [SerializeField] bool cryBool = true;
    [SerializeField] bool CancryBool = true;
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
                // instantiate höyry olio
                var pilvi = Instantiate(piviHahmo, gameObject.transform.position, transform.rotation);
                pilvi.transform.SetParent(allFires.transform);
                if (cloudMoves)
                {
                    pilvi.GetComponentInChildren<MoveBetweenTwoPoints>().shouldMove = true;
                }
                
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
