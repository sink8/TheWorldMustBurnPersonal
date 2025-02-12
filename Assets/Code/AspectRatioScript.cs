using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AspectRatioScript : MonoBehaviour
{
	public float fixedAspectRatio = 16f / 9f;
	//public Camera camera;

	void UpdateFixedAspectRatio() {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		float targetaspect = fixedAspectRatio;

		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleHeight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        Rect rect = new Rect(0, 0, 1, 1); // Default to full screen


        if (scaleHeight < 1.0f)
        {
            // Letterbox (black bars on top/bottom)
            rect.height = scaleHeight;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            // Pillarbox (black bars on left/right)
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.x = (1.0f - scaleWidth) / 2.0f;
        }

        camera.rect = rect;
        camera.clearFlags = CameraClearFlags.SolidColor;

    }

	// Use this for initialization
	void Start() {
        Camera camera = GetComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        UniversalAdditionalCameraData camData = camera.GetUniversalAdditionalCameraData();
        camData.renderType = CameraRenderType.Base; // Ensure it's a base camera
        
        UpdateFixedAspectRatio();
	}

	// Update is called once per frame
	void Update() {
		//if (Input.GetKeyDown(KeyCode.P)) {
		//	UpdateFixedAspectRatio();
		//}
	}
}
