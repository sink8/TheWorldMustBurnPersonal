using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public List<PowerupType> powerups;
    public PowerupType State;
    //public float powerupTime = 20f; // duaration of powerups
    public float powerupTimer; // how many seconds left on the current powerup
    public GameObject player;
    public GameObject weapon;
    public GameObject fm;
    PowerUpProjektile powerup;

    public bool hasTimer = false;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        powerup = FindObjectOfType<PowerUpProjektile>();
    }

    public void ActivatePowerup(PowerupType type) {
        if(powerups.Contains(type)) {
            ActivatePowerupState(type);
        }
    }

    private void Update() {
        if(hasTimer == true) {
        if (powerupTimer > 0) {
            powerupTimer -= Time.deltaTime; // how much time has passed since last update. Eventually it will drop below zero
            if (powerupTimer <= 0) {
                State = PowerupType.None; // we also want this to run only once
                weapon.SetActive(false);
                fm.SetActive(true);
            } 
        }
    }

        if(State == PowerupType.None) {
            weapon.SetActive(false);
            fm.SetActive(true);
        }
    }
    public void ActivatePowerupState(PowerupType type) {
        if (State != PowerupType.None) {
            EndState(State);  // 
        }
        BegingState(type);
        powerupTimer = powerup.powerupTime;  // when powerup is activated time is set
        State = type;  // 
    }

    public void EndState(PowerupType type) {
        State = PowerupType.None;
        weapon.SetActive(false);
    }
    void BegingState(PowerupType type) {
        if (type == PowerupType.Projectile) {
            weapon.SetActive(true);
           
        }

        if (type == PowerupType.NoFire) {
            fm.SetActive(false); 
        }
    }


}
