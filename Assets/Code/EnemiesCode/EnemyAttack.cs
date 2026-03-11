using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackActivationDistance = 4f;
    public GameObject projectilePrefab;
    public Transform pointA;
    public Transform pointB;

    public GameObject player;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float projectileArcHeight = 2f;

    public float speed = 2f;
    public float detectionRadius = 5f;

    public bool attackActivated = false;
    public bool preattackOver = false;
    private Vector3 targetPoint;
    private bool chasingPlayer = false;
    float hahmonscale;
    public bool canMove = true;
    public bool partrolling = false;
    public bool isFacingRight;
    public bool flipDialogue;
    [SerializeField] GameObject dialogue;
    private float initialDialogueScaleX;
    void Start()
    {
        if (dialogue != null) {
        initialDialogueScaleX = Mathf.Abs(dialogue.transform.localScale.x);
            //ApplyDialogueScale();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        targetPoint = pointB.position;


        hahmonscale = gameObject.transform.localScale.x;
        isFacingRight = transform.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            chasingPlayer = true;

            bool playerIsToRight = player.transform.position.x > transform.position.x;

            // Update the bool
            isFacingRight = playerIsToRight;
            if (canMove)
            {
            transform.localScale = new Vector3(
                player.transform.position.x > transform.position.x ? -hahmonscale : hahmonscale,
                transform.localScale.y,
                transform.localScale.z
            );

            }
            // Attack logic
            if (!IsInvoking(nameof(Shoot)))
                InvokeRepeating(nameof(Shoot), 0f, 2f); // fire every 2 seconds
        }
        else
        {
            chasingPlayer = false;
            CancelInvoke(nameof(Shoot));
        }

        if (!chasingPlayer)
        {
            if (canMove)
            {
                Patrol();

            }
        }

        if (flipDialogue && dialogue != null) {
            ApplyDialogueScale();
        }
    }
    void ApplyDialogueScale() {
        float finalScaleX;

        if (isFacingRight == true) {
            // If facing right, force negative scale
            finalScaleX = -initialDialogueScaleX;
        } else {
            // If facing left (natural), force positive scale
            finalScaleX = initialDialogueScaleX;
        }
        if (dialogue != null) {
            //initialDialogueScaleX = Mathf.Abs(dialogue.transform.localScale.x);
        dialogue.transform.localScale = new Vector3(
            finalScaleX,
            dialogue.transform.localScale.y,
            dialogue.transform.localScale.z
        );
            //ApplyDialogueScale();
        }
    }
    void Patrol()
    {
        partrolling= true;

        bool movingToRight = targetPoint.x > transform.position.x;

        // Update the bool
        isFacingRight = movingToRight;

        transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
        {
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
            transform.localScale = new Vector3(targetPoint.x > transform.position.x ? -hahmonscale : hahmonscale, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        ProjectileArc arc = proj.GetComponent<ProjectileArc>();
        if (arc != null)
            arc.Launch(player.transform.position, projectileSpeed, projectileArcHeight);
    }

    public void StopShooting()
    {
        CancelInvoke(nameof(Shoot));
    }
}
