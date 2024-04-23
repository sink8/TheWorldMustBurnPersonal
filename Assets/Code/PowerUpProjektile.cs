using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { None, Projectile, NoFire}
public class PowerUpProjektile : MonoBehaviour
{
    public PowerupType type;
    public GameObject weapon;
    public float powerupTime = 20f;

    void Start()
    {
        //weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //AudioFW.Play("PowerUp");
            FindObjectOfType<GameManager>().ActivatePowerup(type);
        }
        /*weapon.SetActive(true);
        powerupTimer = projectileTime;
        weaponOn = true;*/
    }



}
