using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hover : MonoBehaviour
{
    public float ylös = 0.2f;
    public float speed = 0.5f;
    public Vector3 p;
    void Start()
    {
        p = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var newY = p.y + Mathf.Cos(Time.time * speed) * ylös;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
