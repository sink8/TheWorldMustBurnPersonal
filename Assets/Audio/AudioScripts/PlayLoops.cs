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
            AudioFW.PlayLoop("Level2Cave");
        }
        if (levelNum == 5) {
            //AudioFW.PlayLoop("Level5");
        }
        if (levelNum == 6)
        {
            AudioFW.PlayLoop("Level1Forest_2");
        }
        if (levelNum == 7)
        {
            AudioFW.PlayLoop("Tyypin_track_1");
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
            AudioFW.StopLoop("Level2Cave");
        }
        if (levelNum == 5) {
            AudioFW.StopLoop("Level5");
        }
        if (levelNum == 6)
        {
            AudioFW.StopLoop("Level1Forest_2");
        }
        if (levelNum == 7)
        {
            AudioFW.StopLoop("Tyypin_track_1");
        }
    }
    public void StopAllLevelMusic() {
            AudioFW.StopLoop("Level1Forest");
            AudioFW.StopLoop("Level2Cave");
            AudioFW.StopLoop("Level3Forest");
            AudioFW.StopLoop("Level2Cave");
            AudioFW.StopLoop("Level5");
            AudioFW.StopLoop("Level1Forest_2");
            AudioFW.StopLoop("Tyypin_track_1");
    }


}
