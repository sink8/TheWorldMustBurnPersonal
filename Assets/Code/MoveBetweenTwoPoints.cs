using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveBetweenTwoPoints : RaycastPlatformController
{
    [Header("Movement Settings")]
    public Transform pointA; // Start point
    public Transform pointB; // End point
    public float duration = 2f; // Time to move between points
    public bool shouldMove = false; // Whether the platform should move
    public bool loopMovement = true; // Whether the platform loops back and forth

    public LayerMask passengerMask;
    private Vector2 move;
    private float elapsedTime = 0f;
    private bool movingForward = true; // Tracks movement direction (A -> B or B -> A)
    private List<PassengerMovement> passengerMovement;
    private bool wasMovingPreviously = false; // Tracks if the platform was already moving

    public override void Start()
    {
        base.Start();
        ResetToStartPosition();

    }

    void Update()
    {
        UpdateRaycastOrigins();

        if (shouldMove)
        {
            MovePlatform();
        }
        else
        {
            ResetToStartPosition(); // Ensure platform is always at pointA when stopped
        }
    }

    /// <summary>
    /// Moves the platform smoothly between points A and B.
    /// </summary>
    private void MovePlatform()
    {
        Vector2 positionA = pointA.position;
        Vector2 positionB = pointB.position;

        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / duration;

        // Handle looping or one-way movement
        if (loopMovement)
        {
            // Oscillate between pointA and pointB using Mathf.PingPong
            t = Mathf.PingPong(elapsedTime / duration, 1f);
        }
        else
        {
            // Move from A to B once
            t = Mathf.Clamp01(t);
        }

        // Calculate the target position using interpolation
        Vector2 targetPosition = Vector2.Lerp(movingForward ? positionA : positionB, movingForward ? positionB : positionA, t);

        // Handle direction switching in non-looping mode
        if (!loopMovement && t >= 1f)
        {
            if (movingForward)
            {
                movingForward = false;
            }
            else
            {
                shouldMove = false;
            }

            elapsedTime = 0f; // Reset elapsed time
        }

        // Calculate movement velocity
        move = (targetPosition - (Vector2)transform.position) / Time.deltaTime;

        // Update passenger movement
        CalculatePassengerMovement(move * Time.deltaTime);
        MovePassengers(true);

        // Apply movement
        transform.Translate(move * Time.deltaTime);
        MovePassengers(false);
    }

    /// <summary>
    /// Resets the platform to its starting position (pointA).
    /// </summary>
    private void ResetToStartPosition()
    {
        if (!shouldMove)
        {
            elapsedTime = 0f; // Reset elapsed time
            movingForward = true; // Reset direction to forward (A -> B)
            transform.position = pointA.position; // Snap to pointA
        }
    }

    void MovePassengers(bool beforeMovePlatform)
    {
        foreach (PassengerMovement passenger in passengerMovement)
        {
            if (passenger.moveBeforePlatform == beforeMovePlatform)
            {
                passenger.transform.GetComponent<RayCast2DController>().Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector2 velocity)
    {
        HashSet<Transform> movedPassengers = new HashSet<Transform>();
        passengerMovement = new List<PassengerMovement>();

        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);

        // Vertical movement
        if (velocity.y != 0)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = (directionY == 1) ? velocity.x : 0;
                        float pushY = velocity.y - (hit.distance + skinWidth) * directionY;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector2(pushX, pushY), directionY == 1, true));
                    }
                }
            }
        }

        // Horizontal movement
        if (velocity.x != 0)
        {
            float rayLength = Mathf.Abs(velocity.x) + skinWidth;

            for (int i = 0; i < horizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x - (hit.distance + skinWidth) * directionX;
                        float pushY = 0;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector2(pushX, pushY), false, true));
                    }
                }
            }
        }

        // Passengers on top of the platform
        if (directionY == -1 || (velocity.y == 0 && velocity.x != 0))
        {
            float rayLength = skinWidth * 1.2f;

            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);

                if (hit)
                {
                    if (!movedPassengers.Contains(hit.transform))
                    {
                        movedPassengers.Add(hit.transform);
                        float pushX = velocity.x;
                        float pushY = velocity.y;

                        passengerMovement.Add(new PassengerMovement(hit.transform, new Vector2(pushX, pushY), true, false));
                    }
                }
            }
        }
    }

    public struct PassengerMovement
    {
        public Transform transform;
        public Vector2 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector2 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
        {
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }
}
