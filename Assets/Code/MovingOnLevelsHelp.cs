using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingOnLevelsHelp : MonoBehaviour
{
    [SerializeField] MovingOnLevelsMap moving;
    private bool isAtDestination;
    public GameObject player;
    void Start()
    {
        moving = GetComponent<MovingOnLevelsMap>();
        isAtDestination = false; // Initialize the flag
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == transform.position) {
            moving.enabled = true;
        } else moving.enabled = false;
    }
}
