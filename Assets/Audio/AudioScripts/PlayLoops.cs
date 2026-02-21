using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoops : MonoBehaviour
{
    //public LevelEnd levelEnd;
    public int levelNum;

    void Start()
    {
        //levelEnd = FindObjectOfType<LevelEnd>();
        //levelNum = levelEnd.LevelNumber;


    }

    private void Update() {
        
    }

    public void StartLevelMusic(int levelNum) {

        if (levelNum == 1) {
            AudioFW.PlayLoop("Level1Forest");
        }
        if (levelNum == 2) {
            AudioFW.PlayLoop("Level2Cave");
        }
        if (levelNum == 3) {
            AudioFW.PlayLoop("Level3Forest");
        }

        if (levelNum == 4) {
            AudioFW.PlayLoop("njilnjil_track_1");
        }
        if (levelNum == 5) {
            AudioFW.PlayLoop("njilnjil_track_3");
        }
        if (levelNum == 6)
        {
            AudioFW.PlayLoop("Level2Cave3");
        }
        if (levelNum == 7)
        {
            AudioFW.PlayLoop("njilnjil_track_2");
        }
        if (levelNum == 8) {
            AudioFW.PlayLoop("Level2Night");
        }
        if (levelNum == 9) {
            AudioFW.PlayLoop("Bleeping");
        }
        if (levelNum == 10) {
            AudioFW.PlayLoop("DarkLands");
        }

        if (levelNum == 11) {
            AudioFW.PlayLoop("Etheral");
        }
        if (levelNum == 12) {
            AudioFW.PlayLoop("Exotic");
        }
        if (levelNum == 13) {
            AudioFW.PlayLoop("ForestDrama");
        }
        if (levelNum == 14) {
            AudioFW.PlayLoop("IceSky");
        }
        if (levelNum == 15) {
            AudioFW.PlayLoop("Ascend");
        }

        if (levelNum == 16) {
            AudioFW.PlayLoop("MagicalGateway");
        }
        if (levelNum == 17) {
            AudioFW.PlayLoop("Mystical");
        }
        if (levelNum == 18) {
            AudioFW.PlayLoop("NightFlight");
        }
        if (levelNum == 19) {
            AudioFW.PlayLoop("AncientWaters");
        }
        if (levelNum == 20)
        {
            AudioFW.PlayLoop("Skies");
        }
        if (levelNum == 21) {
            AudioFW.PlayLoop("strato");
        }

        if (levelNum == 22) {
            AudioFW.PlayLoop("awinds");
        }
        if (levelNum == 23) {
            AudioFW.PlayLoop("MagicEscape");
        }
        if (levelNum == 24) {
            AudioFW.PlayLoop("Galaxy");
        }
        if (levelNum == 25) {
            AudioFW.PlayLoop("wholesome");
        }
        if (levelNum == 26) {
            AudioFW.PlayLoop("porrige");
        }
    }

    public void StopLevelMusic() {
        if (levelNum == 1) {
            AudioFW.StopLoop("Level1Forest");
        }
        if (levelNum == 2) {
            AudioFW.StopLoop("Level2Cave");
        }
        if (levelNum == 3) {
            AudioFW.StopLoop("Level3Forest");
        }
        if (levelNum == 4) {
            AudioFW.StopLoop("njilnjil_track_1");
        }
        if (levelNum == 5) {
            AudioFW.StopLoop("njilnjil_track_3");
        }
        if (levelNum == 6)
        {
            AudioFW.StopLoop("Level2Cave3");
        }
        if (levelNum == 7)
        {
            AudioFW.StopLoop("njilnjil_track_2");
        }
        if (levelNum == 8)
        {
            AudioFW.StopLoop("Level2Night");
        }
        if (levelNum == 9) {
            AudioFW.StopLoop("Bleeping");
        }
        if (levelNum == 10) {
            AudioFW.StopLoop("DarkLands");
        }
        if (levelNum == 11) {
            AudioFW.StopLoop("Etheral");
        }
        if (levelNum == 12) {
            AudioFW.StopLoop("Exotic");
        }
        if (levelNum == 13) {
            AudioFW.StopLoop("ForestDrama");
        }
        if (levelNum == 14) {
            AudioFW.StopLoop("IceSky");
        }
        if (levelNum == 15) {
            AudioFW.StopLoop("Ascend");
        }
        if (levelNum == 16) {
            AudioFW.StopLoop("MagicalGateway");
        }
        if (levelNum == 17) {
            AudioFW.StopLoop("Mystical");
        }
        if (levelNum == 18) {
            AudioFW.StopLoop("NightFlight");
        }
        if (levelNum == 19) {
            AudioFW.StopLoop("AncientWaters");
        }
        if (levelNum == 20) {
            AudioFW.StopLoop("Skies");
        }
        if (levelNum == 21) {
            AudioFW.StopLoop("strato");
        }
        if (levelNum == 22) {
            AudioFW.StopLoop("awinds");
        }
        if (levelNum == 23) {
            AudioFW.StopLoop("MagicEscape");
        }
        if (levelNum == 24) {
            AudioFW.StopLoop("Galaxy");
        }
        if (levelNum == 25) {
            AudioFW.StopLoop("wholesome");
        }
        if (levelNum == 26) {
            AudioFW.StopLoop("porrige");
        }
        if (levelNum == 27) {
            AudioFW.StopLoop("njilnjil_track_2");
        }
        if (levelNum == 28) {
            AudioFW.StopLoop("Level2Cave3");
        }

    }
    public void StopAllLevelMusic() {
            AudioFW.StopLoop("Level1Forest");
            AudioFW.StopLoop("Level2Cave");
            AudioFW.StopLoop("Level3Forest");
            AudioFW.StopLoop("Level2Cave");
            AudioFW.StopLoop("Level5");
            AudioFW.StopLoop("Level1Forest_2");
            AudioFW.StopLoop("njilnjil_track_1");
            AudioFW.StopLoop("njilnjil_track_2");
            AudioFW.StopLoop("njilnjil_track_3");
            AudioFW.StopLoop("Level2Cave3");
            AudioFW.StopLoop("Level2Cave2");
            AudioFW.StopLoop("Level2Night");
            AudioFW.StopLoop("Bleeping");
            AudioFW.StopLoop("DarkLands");
            AudioFW.StopLoop("Etheral");
            AudioFW.StopLoop("Exotic");
            AudioFW.StopLoop("ForestDrama");
            AudioFW.StopLoop("IceSky");
            AudioFW.StopLoop("Ascend");
            AudioFW.StopLoop("MagicalGateway");
            AudioFW.StopLoop("Mystical");
            AudioFW.StopLoop("NightFlight");
            AudioFW.StopLoop("AncientWaters");
            AudioFW.StopLoop("Skies");
            AudioFW.StopLoop("strato");
            AudioFW.StopLoop("awinds");
            AudioFW.StopLoop("MagicEscape");
            AudioFW.StopLoop("Galaxy");
            AudioFW.StopLoop("wholesome");
            AudioFW.StopLoop("porrige");
            AudioFW.StopLoop("njilnjil_track_2");
            AudioFW.StopLoop("Level2Cave3");
        
    }


}
