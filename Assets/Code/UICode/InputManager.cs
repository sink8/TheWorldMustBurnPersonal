using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
public static InputManager instance;
[SerializeField] KeyBinding keyBindings; 
    
    private void Awake(){
        if(instance == null){
            instance = this;
        }
        //else if(instance != null){
        //    Destroy(this);
        //}
        //DontDestroyOnLoad(this);
    }

    public KeyCode GetKeyForAction(KeybindingActions keybindingAction){
        foreach (KeyBinding.KeybindingCheck keybindingCheck in keyBindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == keybindingAction){
                return keybindingCheck.keyCode;
            }
        }
        return KeyCode.None;

    }

    public bool GetKeyDown(KeybindingActions key){
        foreach (KeyBinding.KeybindingCheck keybindingCheck in keyBindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == key){
                return Input.GetKeyDown(keybindingCheck.keyCode);
            }
        }

        return false;
    }

    public bool GetKey(KeybindingActions key){
        foreach (KeyBinding.KeybindingCheck keybindingCheck in keyBindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == key){
                return Input.GetKey(keybindingCheck.keyCode);
            }
        }

        return false;
    }

    public bool GetKeyUp(KeybindingActions key){
        foreach (KeyBinding.KeybindingCheck keybindingCheck in keyBindings.keybindingChecks)
        {
            if(keybindingCheck.keybindingAction == key){
                return Input.GetKeyUp(keybindingCheck.keyCode);
            }
        }

        return false;
    }
}
