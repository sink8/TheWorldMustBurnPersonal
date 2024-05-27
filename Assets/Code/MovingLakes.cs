using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLakes : MonoBehaviour
{

    float velocity = 0;
    float force = 0;
    float height = 0;
    float target_height = 0;

    [SerializeField] private float springStiffness = 0.1f;
    [SerializeField] List<WaterSpring> springs = new();

    public float spread = 0.006f;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        foreach(WaterSpring waterSpringComponent in springs)
        {
            //waterSpringComponent.WaveSpringUpdate(springStiffness);
        }
    }

    public void WaveSpringUpdate( float springStiffness)
    {
        height = transform.localPosition.y;
        var x = height - target_height;

        force = - springStiffness * x;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);

    }

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        var x = height - target_height;
        var loss = -dampening * velocity;
        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y+velocity, transform.localPosition.z);
    }
}
