using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    
    public int horizontalRayDefault;
    public int verticalRayDefault;
    public float burnRadiusDefault;
    public float burnRadiusEnd;
    public float timer = 0;
    public float explosionTime= 2f;
    public float explosionSpeed= 2f;
    public bool explosionHappening = false;
    RayCast2DController raycastController;
    FireManager fm;

    void Start()
    {
    
    raycastController = FindObjectOfType<RayCast2DController>();
    fm = FindObjectOfType<FireManager>();

    burnRadiusDefault = fm.burnRadius;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.K)){
            explosionHappening = true;
         }
        if(explosionHappening == true){
            Explosion();
        }

    }

    public void Explosion(){

        //timer += Time.deltaTime;

        if(timer > 0){
            timer -= Time.deltaTime;
            fm.burnRadius += explosionSpeed * Time.deltaTime;
        }
        else{
            fm.burnRadius = burnRadiusDefault;
            explosionHappening = false;
            timer = explosionTime;

        }
    }



}//class
