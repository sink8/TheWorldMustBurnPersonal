using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretColorSwitch : MonoBehaviour
{
    public bool colorSwitchRed, colorSwitchGreen, colorSwitchBlue, colorSwitchPurple, colorSwitchBlack, colorSwitchWhite = false;
    RayCastPlayer player;
    SaveUI saveUI;
    void Start()
    {
        player = FindObjectOfType<RayCastPlayer>();
        saveUI = FindObjectOfType<SaveUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {

            if (colorSwitchRed) { player.ToggleColorToRed();
                saveUI.isRed = true;
                saveUI.colorChanged = true;
                player.ToggleColorToRed();
            }
            if (colorSwitchBlue) { player.ToggleColorToBlue();
                saveUI.isBlue = true;
                saveUI.colorChanged = true;
                player.ToggleColorToBlue();
            }
            if (colorSwitchPurple) { player.ToggleColorToPurple();
                saveUI.isPurple = true;
                saveUI.colorChanged = true;
                player.ToggleColorToPurple();
            }
            if (colorSwitchGreen) { player.ToggleColorToGreen();
                saveUI.isGreen = true;
                saveUI.colorChanged = true;
                player.ToggleColorToGreen();
            }
            if (colorSwitchBlack) { player.ToggleColorToBlack();
                saveUI.isBlack = true;
                saveUI.colorChanged = true;
                player.ToggleColorToBlack();
            }
            if (colorSwitchWhite) { player.ToggleColorToWhite();
                saveUI.isWhite = true;
                saveUI.colorChanged = true;
                player.ToggleColorToWhite();
            }
        }


    }
}
