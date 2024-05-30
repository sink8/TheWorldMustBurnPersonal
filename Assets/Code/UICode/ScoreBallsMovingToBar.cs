using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBallsMovingToBar : MonoBehaviour
{
    public RectTransform uiTarget; // The target UI element
    public Canvas canvas; // The Canvas containing the UI element
    public Vector2 spawnAreaMin; // Minimum bounds of the spawn area
    public Vector2 spawnAreaMax; // Maximum bounds of the spawn area
    //public GameObject targetPosition; // The fixed target position
    public Vector2 targetPosition; // The target position in world coordinates

    public float speed = 1f; // Speed of movement
    public float hoverMagnitude = 0.5f; // Magnitude of the hovering effect
    public float hoverFrequency = 1f; // Frequency of the hovering effect
    void Start()
    {
        //targetPosition = GameObject.Find("BallTargetPos");
        GameObject tempObject = GameObject.Find("ScoreUIprefab");
        GameObject tempObject2 = GameObject.Find("StartPoint");

        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            canvas = tempObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }
        if (tempObject2 != null)
        {
            //If we found the object , get the Canvas component from it.
            uiTarget = tempObject2.GetComponent<RectTransform>();
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject2.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HoveringTOPostion();
    }

    void HoveringTOPostion()
    {
        if (uiTarget != null && canvas != null)
        {
            // Get the target position from the target GameObject
            //Vector2 targetPosition1 = targetPosition.transform.position;
            Vector2 targetPosition1 = UIUtilities.GetWorldPositionFromUI(canvas, uiTarget);
            Vector2 direction = (targetPosition1 - (Vector2)transform.position).normalized;

            // Calculate the direction to the target
            //Vector2 direction = (targetPosition1 - (Vector2)transform.position).normalized;

            // Calculate the hovering offset
            float hoverOffsetX = Mathf.Sin(Time.time * hoverFrequency) * hoverMagnitude;
            float hoverOffsetY = Mathf.Cos(Time.time * hoverFrequency) * hoverMagnitude;

            // Apply the hovering effect to the direction
            Vector2 hoveringDirection = direction + new Vector2(hoverOffsetX, hoverOffsetY);

            // Move the sprite towards the target position with the hovering effect
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + hoveringDirection, speed * Time.deltaTime);

            // Optional: Destroy the sprite when it reaches the target position
            if (Vector2.Distance(transform.position, targetPosition1) < 0.1f)
            {
                Destroy(gameObject); // Destroy or handle the sprite as needed
            }
        }
    }




    public static class UIUtilities
    {
        public static Vector2 GetWorldPositionFromUI(Canvas canvas, RectTransform uiElement)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, uiElement.position);
            Vector3 worldPoint = canvas.worldCamera.ScreenToWorldPoint(screenPoint);
            return new Vector2(worldPoint.x, worldPoint.y);
        }
    }
}

