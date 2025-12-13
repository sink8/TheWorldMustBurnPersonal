using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreScores : MonoBehaviour
{
    public int levelAmount = 33;



    public Text bronceText;
    public Text silverText;
    public Text goldText;
    //public Text LevelTitle;
    public TMP_Text LevelTitle;


    public int tileamountlv1 = 1;
    public int tileamountlv2 = 1;
    public int tileamountlv3 = 1;
    public int tileamountlv4 = 1;
    public int tileamountlv5 = 1;

    // should make highscores etc. as array/list. Just as levels are


    public float[] bronceHighScores;
    public float[] silverHighScores;
    public float[] goldHighScores;

    public float[] bronceHighSeconds;
    public float[] silverHighSeconds;
    public float[] goldHighSeconds;
    public int[] secrets;
    public bool[] levelsFinished;
    public int[] tileAmounts;

    public GameObject[] levels;
    public GameObject[] locks;
    public GameObject[] finished;

    public GameObject player;
    bool movePlayer = false;
    public string levelNameX;
    public GameObject ScoreCanvas;

    [SerializeField] int howManyFlowersNeedsToBeFound_1 = 7;

    [SerializeField] int howManyForBurnEnd = 10;
    public int countForTheEnd = 0;


    private async void Start() {
        //levels = GameObject.FindGameObjectsWithTag("Level");

        bronceHighScores = new float[levelAmount];
        silverHighScores = new float[levelAmount];
        goldHighScores = new float[levelAmount];
        bronceHighSeconds = new float[levelAmount];
        silverHighSeconds = new float[levelAmount];
        goldHighSeconds = new float[levelAmount];
        secrets = new int[levelAmount];
        levelsFinished = new bool[levelAmount];
        //locks = GameObject.FindGameObjectsWithTag("Locks");


    }

    void Update() {

        UpdateScores();
        /*L1pronceText.text = Mathf.RoundToInt((l1HighScorePronce) * 100) + " % " + Mathf.RoundToInt(l1SecondsPronce / 60) + " min " + Mathf.RoundToInt(l1SecondsPronce % 60) + " s ";
        L1SilverText.text = Mathf.RoundToInt((l1HighScoreSilver) * 100) + " % " + Mathf.RoundToInt(l1SecondsSilver / 60) + " min " + Mathf.RoundToInt(l1SecondsSilver % 60) + " s ";
        L1GoldText.text = Mathf.RoundToInt((l1HighScoreGold) * 100) + " % " + Mathf.RoundToInt(l1SecondsGold / 60) + " min " + Mathf.RoundToInt(l1SecondsGold % 60) + " s ";

        L2pronceText.text = Mathf.RoundToInt((l2HighScorePronce) * 100) + " % " + Mathf.RoundToInt(l2SecondsPronce / 60) + " min " + Mathf.RoundToInt(l2SecondsPronce % 60) + " s ";
        L2SilverText.text = Mathf.RoundToInt((l2HighScoreSilver) * 100) + " % " + Mathf.RoundToInt(l2SecondsSilver / 60) + " min " + Mathf.RoundToInt(l2SecondsSilver % 60) + " s ";
        L2GoldText.text = Mathf.RoundToInt((l2HighScoreGold) * 100) + " % " + Mathf.RoundToInt(l2SecondsGold / 60) + " min " + Mathf.RoundToInt(l2SecondsGold % 60) + " s ";


        L3pronceText.text = Mathf.RoundToInt((l3HighScorePronce) * 100) + " % " + Mathf.RoundToInt(l3SecondsPronce / 60) + " min " + Mathf.RoundToInt(l3SecondsPronce % 60) + " s ";
        L3SilverText.text = Mathf.RoundToInt((l3HighScoreSilver) * 100) + " % " + Mathf.RoundToInt(l3SecondsSilver / 60) + " min " + Mathf.RoundToInt(l3SecondsSilver % 60) + " s ";
        L3GoldText.text = Mathf.RoundToInt((l3HighScoreGold) * 100) + " % " + Mathf.RoundToInt(l3SecondsGold / 60) + " min " + Mathf.RoundToInt(l3SecondsGold % 60) + " s ";
        */

        // make these better later. Doesn't need to run on every frame, just occasionally. For example, run some function when level ends.
        if(SaveManager.instance.hasLoaded == true){
            bronceHighScores = SaveManager.instance.activeSave.bronceHighScoresSave;
            silverHighScores = SaveManager.instance.activeSave.silverHighScoresSave;
            goldHighScores = SaveManager.instance.activeSave.goldHighScoresSave;
            bronceHighSeconds = SaveManager.instance.activeSave.bronceHighSecondsSave;
            silverHighSeconds = SaveManager.instance.activeSave.silverHighSecondsSave;
            goldHighSeconds = SaveManager.instance.activeSave.goldHighSecondsSave;
            secrets = SaveManager.instance.activeSave.secretsFound;
            levelsFinished = SaveManager.instance.activeSave.levelFinished;
            if(movePlayer == false){
                var xpos = SaveManager.instance.activeSave.respawnPosition[0];
                var ypos = SaveManager.instance.activeSave.respawnPosition[1];
                var zpos = SaveManager.instance.activeSave.respawnPosition[2];
                player.transform.position = new Vector3(xpos,ypos,zpos);
                movePlayer = true;
            }
        }

        for (int j = 0; j < levelAmount; j++) {


            if (bronceHighScores[j] >= 0.1 || silverHighScores[j] >= 0.1 || goldHighScores[j] >= 0.1) {
                levels[j+1].GetComponent<MovingOnLevelsMap>().locked = false;
                locks[j+1].SetActive(false);
                finished[j].GetComponent<Renderer>().sortingOrder = 40;
            }
        }

        for (int K = 0; K < levelAmount; K++)
        {

            if ( goldHighScores[K] >= 0.1)
            {
                countForTheEnd++;
            }
        }

        // secrets, how will those be opened. Shitty way but I'm in hurry
        if (SecretManager.Instance.secretsFound >= 7)
        {
            levels[30].GetComponent<MovingOnLevelsMap>().locked = false;
            locks[30].SetActive(false);
            finished[30].GetComponent<Renderer>().sortingOrder = 40;
        }

        if (SecretManager.Instance.secretsFound >= 17)
        {
            levels[31].GetComponent<MovingOnLevelsMap>().locked = false;
            locks[31].SetActive(false);
            finished[31].GetComponent<Renderer>().sortingOrder = 40;
        }

        if (SecretManager.Instance.secretsFound >= 30)
        {
            levels[32].GetComponent<MovingOnLevelsMap>().locked = false;
            locks[32].SetActive(false);
            finished[32].GetComponent<Renderer>().sortingOrder = 40;
        }

        //if (bronceHighScores[0] >= 0.5 || silverHighScores[0] >= 0.5 || goldHighScores[0] >= 0.5) {
        //    levels[1].GetComponent<MovingOnLevelsMap>().locked = false;
        //    locks[1].SetActive(false);
        //}

        //if (bronceHighScores[1] >= 0.5 || silverHighScores[1] >= 0.5 || goldHighScores[1] >= 0.5) {
        //    levels[2].GetComponent<MovingOnLevelsMap>().locked = false;
        //    locks[2].SetActive(false);
        //}
    }

    public void UpdateScores() {
        for(int i = 0; i < levels.Length; i++) {
            if (player.transform.position == levels[i].transform.position) {
                if (bronceHighScores[i] == 0 && silverHighScores[i] == 0 && goldHighScores[i] == 0) {
                    print("scores");
                    ScoreCanvas.SetActive(false);
                } else { ScoreCanvas.SetActive(true); }

                print("same area");

                bronceText.text = Mathf.RoundToInt((bronceHighScores[i]) * 100) + " % " + Mathf.FloorToInt(bronceHighSeconds[i] / 60) + " min " + Mathf.RoundToInt(bronceHighSeconds[i] % 60) + " s ";
                silverText.text = Mathf.RoundToInt((silverHighScores[i]) * 100) + " % " + Mathf.FloorToInt(silverHighSeconds[i] / 60) + " min " + Mathf.RoundToInt(silverHighSeconds[i] % 60) + " s ";
                goldText.text = Mathf.RoundToInt((goldHighScores[i]) * 100) + " % " + Mathf.RoundToInt(goldHighSeconds[i] / 60) + " min " + Mathf.RoundToInt(goldHighSeconds[i] % 60) + " s ";
                //LevelTitle.text = "Highscores level  " + (i + 1);
                LevelTitle.text = "Highscores level  " + (i + 1 ) + "-" + levelNameX;

            }
        }
    }

}
