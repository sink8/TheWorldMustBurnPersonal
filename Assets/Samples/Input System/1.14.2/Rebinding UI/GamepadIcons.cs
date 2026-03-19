using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    // This allows you to right-click in the Project window to create the asset
    [CreateAssetMenu(fileName = "GamepadIcons", menuName = "Input System/Gamepad Icons")]
    public class GamepadIcons : ScriptableObject
    {
        public Sprite buttonSouth;
        public Sprite buttonNorth;
        public Sprite buttonEast;
        public Sprite buttonWest;
        // Add more fields here for D-pad, Bumpers, etc. if needed

        public Sprite GetSprite(string controlPath)
        {
            // This is the logic the Example script is looking for
            if (string.IsNullOrEmpty(controlPath)) return null;

            // Simple check for the "South" button (A on Xbox, Cross on PS)
            if (controlPath.EndsWith("buttonSouth")) return buttonSouth;
            if (controlPath.EndsWith("buttonNorth")) return buttonNorth;
            if (controlPath.EndsWith("buttonEast")) return buttonEast;
            if (controlPath.EndsWith("buttonWest")) return buttonWest;

            return null;
        }
    }
}
