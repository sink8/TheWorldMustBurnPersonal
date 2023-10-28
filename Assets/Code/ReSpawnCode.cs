using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawnCode : MonoBehaviour
{

    public bool respawnActivated = false;
    public GameObject reSpawnObject;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("osui");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("player osui");
            ReSpawnPoint();
        }

    }

    void ReSpawnPoint()
    {
        respawnActivated = true;

    }
}
