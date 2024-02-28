using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public ParticleSystem rain;
    public float rainCycle = 9;
    public float timer = 0;
    public bool rainBool = true;

    PlayerHealth playerHealth;

    private void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("Water")) {
            print("rain hit the player");
            playerHealth.Damaged(1);
        }

    }

    private void Update() {
        timer += Time.deltaTime;
        while(timer > rainCycle) {
            if (rainBool == true) {
                AudioFW.StopLoop("Rain");
                rain.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainBool = false;
            } else {
                rain.Play();
                AudioFW.PlayLoop("Rain");
                rainBool = true;
            }
            timer -= rainCycle;

        }
    }

}
