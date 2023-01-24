using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "KeyBinding", menuName = "TWMB/KeyBinding", order = 0)]
public class KeyBinding : ScriptableObject {
    
    [System.Serializable]
    public class KeybindingCheck{

        public KeybindingActions keybindingAction;
        public KeyCode keyCode;
    }


    public KeybindingCheck[] keybindingChecks;
}


