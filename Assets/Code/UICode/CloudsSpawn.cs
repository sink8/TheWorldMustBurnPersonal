using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawn : MonoBehaviour
{
    CloudMoving cloudScript;
    [SerializeField] float cloudSpeed = 1f;

    [SerializeField] float spawnInterval;
    [SerializeField] GameObject[] clouds;
    [SerializeField] GameObject[] startPoints;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject allClouds;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Invoke("AttemptSpawn", spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCloud(){
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length - 1)]);
        var speedRange = Random.Range(0.5f, 1.3f);
        var frequenceRange = Random.Range(0.6f, 1.3f);
        cloud.GetComponent<CloudMoving>().cloudSpeed = speedRange;
        cloud.GetComponent<CloudMoving>().frequency = frequenceRange;
        var startPointIndex = Random.Range(0,startPoints.Length);
        cloud.transform.SetParent(allClouds.transform);
        cloud.transform.position = startPoints[startPointIndex].transform.position;
    }

    void AttemptSpawn(){
        SpawnCloud();
        Invoke("AttemptSpawn", spawnInterval);
    }
}
