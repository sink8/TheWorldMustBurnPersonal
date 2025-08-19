using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] /// this script will also run in the editor. but normal update function doesn't run in editor
public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("Parallax scroll speed relative to camera movement")]
    public float speedX;
    public float speedY;

    Transform cameraTransform;
    Vector3 previousCameraPosition;
    bool previousMoveParallaxLayers;
    ParallaxOptions po;
    public Camera cam;
    

    void OnEnable() {
        //cam = Camera.main;
        cameraTransform = cam.transform;
        po = cam.GetComponent<ParallaxOptions>();
        previousCameraPosition = cameraTransform.position;
    }

    void Update() {
        if (po.moveParallaxLayers && !previousMoveParallaxLayers)
            previousCameraPosition = cameraTransform.position;

        previousMoveParallaxLayers = po.moveParallaxLayers;

        if (!Application.isPlaying && !po.moveParallaxLayers)
            return;

        var delta = cameraTransform.position - previousCameraPosition;
        transform.position += Vector3.Scale(delta, new Vector3(speedX, speedY));
        previousCameraPosition = cameraTransform.position;
    }
}
