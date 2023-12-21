using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{

    Text livesText;
    PlayerHealth health1;
    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<Text>();
        health1 = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives " + health1.health;
    }
}
