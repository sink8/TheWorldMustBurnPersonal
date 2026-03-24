using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAhead : MonoBehaviour
{
    [Header("Peek Settings")]
    [Tooltip("How far the camera shifts from the center.")]
    [SerializeField] private float peekDistance = 4f;

    [Tooltip("How smoothly the camera moves (Higher = Faster).")]
    [SerializeField] private float smoothing = 5f;

    private CinemachineVirtualCamera vcam;
    private CinemachineFramingTransposer transposer;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        // Framing Transposer is the 'Body' component used for 2D
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (transposer == null)
        {
            Debug.LogError("CameraPeekSystem: Body must be set to 'Framing Transposer'!");
        }
    }

    void Update()
    {
        if (UserInput.instance == null || transposer == null) return;

        // 1. Grab the AimInput Vector2 from your UserInput script
        Vector2 input = UserInput.instance.AimInput;

        // 2. Calculate the target offset
        // We multiply the raw input (-1 to 1) by our peek distance
        Vector3 targetOffset = new Vector3(input.x, input.y, 0) * peekDistance;

        // 3. Smoothly move the Tracked Object Offset toward that target
        transposer.m_TrackedObjectOffset = Vector3.Lerp(
            transposer.m_TrackedObjectOffset,
            targetOffset,
            Time.deltaTime * smoothing
        );
    }
}
