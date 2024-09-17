using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public ParticleSystem rain;
    public float rainCycle = 10;
    public float timer = 0;
    public float raintime = 4;
    public bool rainBool = true;

    PlayerHealth playerHealth;

    private void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        timer = rainCycle;
    }

    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("Water")) {
            print("rain hit the player");
            playerHealth.Damaged(1);
        }

    }

    private void Update() {

        timer += Time.deltaTime;
        if (!rainBool && timer >= rainCycle) {
            print("coroutine started");
            StartCoroutine(PlayRain());
        }

        //timer += time.deltatime;
        //while (timer > raincycle)
        //{
        //    if (rainbool == true)
        //    {
        //        audiofw.stoploop("rain");
        //        rain.stop(true, particlesystemstopbehavior.stopemitting);
        //        rainbool = false;
        //    }
        //    else
        //    {
        //        rain.play();
        //        audiofw.playloop("rain");
        //        rainbool = true;
        //    }
        //    timer -= raincycle;

        //}
    }

    IEnumerator PlayRain()
    {
        rainBool = true;
        rain.Play();
        AudioFW.PlayLoop("Rain");
        yield return new WaitForSeconds(raintime);

        AudioFW.StopLoop("Rain");
        rain.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        timer = 0;
        rainBool = false;

    }

}
