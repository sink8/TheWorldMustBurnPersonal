using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoving : MonoBehaviour
{
    public float cloudSpeed = 0.7f;
    public float frequency = 1f;
    [SerializeField] float magnitude = 0.5f;

    Vector3 pos, localScale;

    void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();
    }

    void MoveRight(){
        pos += transform.right * Time.deltaTime * cloudSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
