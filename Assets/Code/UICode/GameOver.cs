using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            //Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }

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


}
