using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
public static InputManager instance;
[SerializeField] KeyBinding keyBindings;
    public bool usingController = false;
    
    
    private void Awake(){
        if(instance == null){
            instance = this;
        }

        
        //else if(instance != null){
        //    Destroy(this);
        //}
        //DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
  

        if (Gamepad.all.Count > 0)
        {
            usingController = true;
        }
        else
        {
            usingController = false;
        }

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
