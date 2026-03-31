using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    bool isFullScreen = true;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //Application.Quit();
        }
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SceneManager.LoadScene(0);
        //    Time.timeScale = 1;
        //}

        //SwitchToWindowed();

    }

    public void Retry() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        //ScoreCounter.scoreValue = 0;
    }

    public void QuitGame() {
        Application.Quit();
        Debug.LogError("Game quit!");
    }

    public void SwitchToWindowed()
    {
        //if (Input.GetKeyDown(KeyCode.B) && isFullScreen == true )
            if ( isFullScreen == true )
        {
            Screen.fullScreen = !Screen.fullScreen;
            isFullScreen = false;
        }

        //if(Input.GetKeyDown(KeyCode.B) && isFullScreen == false)
            if ( isFullScreen == false)
        {
            Screen.fullScreen = !Screen.fullScreen;
            isFullScreen = true;
        }

        //if (Input.GetKeyDown(KeyCode.N) )
        //{
        //    Screen.SetResolution(1920, 1080, Screen.fullScreen);
        //}
        
    }

    public void SwichBackTo1920()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
}
