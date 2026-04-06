using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.VirtualTexturing;

public class Loppu2 : MonoBehaviour
{
    public ParticleSystem[] systems;
    [SerializeField] GameObject player;
    [SerializeField] RayCastPlayer castPlayer;
    public GameObject loppucanvas;
    public GameObject loppucanvasFirst;
    [SerializeField] Animator animator;
    [SerializeField] float soundDelay = 3.0f;
    //[SerializeField] Animator animatorplayer;

    public Transform targetPosition; // Drag an empty GameObject here to act as the destination
    public float speed = 5f;
    private bool shouldMove = false;
    public float timer;
    float duration = 17f;
    private float[] initialRates;

    void OnEnable()
    {
        initialRates = new float[systems.Length];
        for (int i = 0; i < systems.Length; i++)
        {
            initialRates[i] = systems[i].emission.rateOverTime.constant;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove && targetPosition != null)
        {
            castPlayer.enabled = false;
            PlayerMovestoKOKKO();
            
        }

        if (shouldMove && timer < duration)
        {
            timer += Time.deltaTime;

            // Calculate how much to fade (1.0 at start, 0.0 at 20 seconds)
            float percentage = Mathf.Clamp01(1f - (timer / duration));

            for (int i = 0; i < systems.Length; i++)
            {
                var emission = systems[i].emission;

                // You must assign a new MinMaxCurve, you can't just multiply the old one
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(initialRates[i] * percentage);
            }
        }
        else if (timer >= duration)
        {

            shouldMove = false; // Stop updating once we hit zero
            if (loppucanvas != null) {
                loppucanvas.SetActive(true);
                EventSystem.current.SetSelectedGameObject(loppucanvasFirst); 
            }
        }
    }

    IEnumerator PlaySoundWithDelay()
    {
        // 1. Wait for the specified amount of seconds
        yield return new WaitForSeconds(soundDelay);

        // 2. Play the sound
        AudioFW.Play("Kokko");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("kokkoseisoo");
            shouldMove = true;
        StartCoroutine(PlaySoundWithDelay());
        }

    }

    void PlayerMovestoKOKKO()
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition.position,
                speed * Time.deltaTime
            );

        // Optional: Stop moving once we are basically there
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.001f)
        {
            shouldMove = false;
        }
    }
}
