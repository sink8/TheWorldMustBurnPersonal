using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIFollow : MonoBehaviour
{
    [Header("Pathfinding")]

    public Transform target;
    public float activateDistance = 50f;
    public float pathUpadateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModiefier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behaviour")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    Path path;
    int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpadateSeconds);
    }

    void FixedUpdate() {
        if(TargetInDistance() && followEnabled) {
            PathFollow();
        }
    }
    
    void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow() {
        if(path == null){
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count) {
            return;
        }

        Vector3 startOffset = transform.position - new Vector3(jumpCheckOffset, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if(jumpEnabled && isGrounded) {
            if(direction.y > jumpNodeHeightRequirement) {
                rb.AddForce(Vector2.up * speed * jumpModiefier);
            }
        }
        //movement
        rb.AddForce(force);

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        if (directionLookEnabled) {
            if(rb.velocity.x > 0.05f) {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if(rb.velocity.x < -0.05f) {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

    }

    private bool TargetInDistance() {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
}
