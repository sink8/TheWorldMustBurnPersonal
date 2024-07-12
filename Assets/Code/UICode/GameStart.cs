using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{

    [SerializeField] float Timer = 1f; // 
    public bool birth = false;
    public GameObject player;
    GameManager gm;

    public GameObject playerParticles;
    public GameObject lightP;
    public ParticleSystem spark;

    
    //RayCastPlayer playerC;
    private void Start() {
        gm = FindObjectOfType<GameManager>();
        gm.State = PowerupType.None;

    }

    void Update()
    {
        if (Timer > 0) {
            Timer -= Time.deltaTime; // how much time has passed since last update. Eventually it will drop below zero

            if (Timer <= 0) {
                //player.SetActive(true);
                Timer = 0;
                player.GetComponent<RayCastPlayer>().enabled = true;
                playerParticles.SetActive(true);
                lightP.SetActive(true);
                spark.Play();
                //player.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

    }
}
