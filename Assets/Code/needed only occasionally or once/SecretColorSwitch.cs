using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretColorSwitch : MonoBehaviour
{
    public bool colorSwitchRed, colorSwitchGreen, colorSwitchBlue, colorSwitchBlueReal, colorSwitchPurple, colorSwitchBlack, colorSwitchWhite = false;
    public RayCastPlayer player;
    public SaveUI saveUI;
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
                saveUI.isBlue = false;
                saveUI.isRed = true;
                saveUI.isPurple = false;
                saveUI.isWhite = false;
                saveUI.isBlack = false;
                saveUI.isGreen = false;
                saveUI.colorChanged = true;
                player.ToggleColorToRed();
            }
            if (colorSwitchBlue) { player.ToggleColorToBlue();
                saveUI.isBlue = true;
                saveUI.colorChanged = true;
                player.ToggleColorToBlue();
            }

            if (colorSwitchBlueReal) {
                player.ToggleColorToBlueReal();
                saveUI.isBlue = true;
                saveUI.isRed = false;
                saveUI.isPurple = false;
                saveUI.isWhite = false;
                saveUI.isBlack = false;
                saveUI.isGreen = false;

                saveUI.colorChanged = true;
                player.ToggleColorToBlueReal();
            }
            if (colorSwitchPurple) { player.ToggleColorToPurple();
                saveUI.isBlue = false;
                saveUI.isRed = false;
                saveUI.isPurple = true;
                saveUI.isWhite = false;
                saveUI.isBlack = false;
                saveUI.isGreen = false;
                saveUI.colorChanged = true;
                player.ToggleColorToPurple();
            }
            if (colorSwitchGreen) { player.ToggleColorToGreen();
                saveUI.isBlue = false;
                saveUI.isRed = false;
                saveUI.isPurple = false;
                saveUI.isWhite = false;
                saveUI.isBlack = false;
                saveUI.isGreen = true;
                saveUI.colorChanged = true;
                player.ToggleColorToGreen();
            }
            if (colorSwitchBlack) { player.ToggleColorToBlack();
                saveUI.isBlue = false;
                saveUI.isRed = false;
                saveUI.isPurple = false;
                saveUI.isWhite = false;
                saveUI.isBlack = true;
                saveUI.isGreen = false;
                saveUI.colorChanged = true;
                player.ToggleColorToBlack();
            }

            if (colorSwitchWhite) { player.ToggleColorToWhite();
                saveUI.isBlue = false;
                saveUI.isRed = false;
                saveUI.isPurple = false;
                saveUI.isWhite = true;
                saveUI.isBlack = false;
                saveUI.isGreen = false;
                saveUI.colorChanged = true;
                player.ToggleColorToWhite();
            }
        }


    }
}
