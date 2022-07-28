using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemyAttack : MonoBehaviour
{
    EnemyAIFollow follow;
    public float attackActivationDistance = 4f;
    public GameObject target;
    public ParticleSystem waterAttack;
    public ParticleSystem waterImu;
    public float timeForTheAttack = 3f;
    public float attackTime = 2f;
    public float timer = 0f;
    public float timerAttack = 0f;
    public bool attackActivated = false;
    public bool preattackOver = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        follow = FindObjectOfType<EnemyAIFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetInDistance();
    }

    private void TargetInDistance() {
        if( Vector2.Distance(transform.position, target.transform.position) < attackActivationDistance ){
            print("tarpeeksi lähellä");
            attackActivated = true;
            follow.followEnabled = false;
            follow.jumpEnabled = false;
        }

        if(attackActivated == true && preattackOver == false){
            timer += Time.deltaTime;
                CreateWaterAttackPre();
        }
            if(timer >= timeForTheAttack){
                preattackOver = true;

            }
            if(preattackOver == true){
                CreateWaterAttackEffect();
                timerAttack += Time.deltaTime;
            }
            if(timerAttack >= attackTime){
                timer = 0;
                timerAttack = 0;
                attackActivated = false;
                preattackOver = false;
                follow.followEnabled = true;
                follow.jumpEnabled = true;
                    
            }

        // enemy should wait a bit after the attack, before it starts to move again

    }

    void ActivateAttack(){



    }

    void CreateWaterAttackEffect() {
        waterAttack.Play();
    }
    void CreateWaterAttackPre() {
        waterImu.Play();
    }
}
