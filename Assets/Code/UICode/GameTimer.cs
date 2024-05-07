using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float gameTime = 300;
    public float timer = 0;
    //public float winPoints = 30;

    [SerializeField]
    Text countDownText;
    void Start()
    {
        //timer = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        timer += 1 * Time.deltaTime;
        countDownText.text = min.ToString("00") + ":" + sec.ToString("00") ;

        //if (timer <= 0) {
          //  timer = 0;

/*            if (ScoreCounter.scoreValue < winPoints) {
                print(" you lose ");
                //lose.SetActive(true);
            } else {
                print(" you win ");
                //win.SetActive(true);
            }*/
       // }
    }
}
