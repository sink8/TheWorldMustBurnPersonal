using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int levelNumber = 1;
    public float scoreValue = 0;
    public int secretValue = 0;
    Text score;
    public Text flowers;
    public GameObject flowerso;
    public GameObject timer;
    public FireManager fm;
    GameTimer gameTimer;
    StoreScores storeScores;
    [SerializeField] int burnableTilesCount;
    int secretsInThisLevel;
    public float runningScore;
    public float perScore;
    public Image scoreBar;
    [SerializeField] Gradient scorebarColor;

    public GameObject bronce, silver, gold;

    public Material materialBar;
    void Start()
    {
        if(SaveManager.instance.activeSave.levelFinished[levelNumber - 1] == true)
        {
            timer.SetActive(true);
        }
        materialBar.SetColor("_Color_1", new Color(191, 22, 0, 1));
        fm = FindObjectOfType<FireManager>();
        burnableTilesCount = fm.GetComponent<FireManager>().GetTileAmountSprite();
        secretsInThisLevel = fm.GetComponent<FireManager>().GetSecretAmountSprite();
        print(burnableTilesCount);
        score = GetComponent<Text>();
        gameTimer = FindObjectOfType<GameTimer>();
        storeScores = FindObjectOfType<StoreScores>();
        scoreValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // runningScore = Mathf.RoundToInt((scoreValue / burnableTilesCount) * 100);
        // int percentage = Mathf.FloorToInt((scoreValue / burnableTilesCount) * 100);
        // score.text = (percentage == 100 ? 100 : Mathf.Min(percentage, 99)) + " %";
        //score.text =   Mathf.RoundToInt((scoreValue / burnableTilesCount)*100) + " %";

        float rawPercent = (scoreValue / burnableTilesCount) * 100f;
        int percentage = Mathf.FloorToInt(rawPercent);
        runningScore = percentage;  
        score.text = percentage + " %";

        var levelsecNum = SecretManager.Instance.GetTotalFoundSecretsLevel(levelNumber);
        flowers.text =  levelsecNum +  "/" + SaveManager.instance.Maxsecrets[levelNumber - 1];
        if(levelsecNum > 0)
        {
            flowerso.SetActive(true);
        }

        if (runningScore < 50) {
            //materialBar.SetFloat("_Color_1", -2);
            
            //silver.SetActive(false);
            //bronce.SetActive(false);
            //gold.SetActive(false);
        }
        else if(runningScore >= 50 && runningScore < 75) {
            //bronce.SetActive(true);
        } else if (runningScore >= 75 && runningScore < 100) {
            //silver.SetActive(true);
            //bronce.SetActive(false);
        } else if (runningScore >= 100) {
            materialBar.SetColor("_Color_1", new Color(191, 0, 0, 1));
            
            //silver.SetActive(false);
            //bronce.SetActive(false);
            //gold.SetActive(true);
        }

        scorebarFiller();

    }

    public void RegisterFlowers(int i)
    {
        SaveManager.instance.activeSave.flowersFound[i - 1] = fm.secretsFound;
    }

    public void RegisterScore() {
        for (int i = 0; i < 3; i++) {
            if (levelNumber == i + 1) {
                if (perScore >= 0.1 && perScore < 0.75) {
                    if (perScore > storeScores.bronceHighScores[i]) {
                        storeScores.bronceHighScores[i] = perScore;
                        storeScores.bronceHighSeconds[i] =  gameTimer.timer;
                    }
                }
                if (perScore >= 0.75 && perScore < 1) {
                    if (perScore > storeScores.silverHighScores[i]) {
                        storeScores.silverHighScores[i] = perScore;
                        storeScores.silverHighSeconds[i] = gameTimer.timer;
                    }
                }
                if (perScore >= 1) {
                    if (perScore > storeScores.goldHighScores[i]) {
                        storeScores.goldHighScores[i] = perScore;
                        storeScores.goldHighSeconds[i] = gameTimer.timer;
                    }
                }


            }
        }
    }

    public async void RegisterNewScore(int i) {
        //for(int i = 0; i < storeScores.levels.Length; i++) {
        SaveManager.instance.activeSave.levelFinished[i-1] = true;
            perScore = scoreValue / burnableTilesCount;
            if (perScore >= 0.10 && perScore < 0.75) {
                if (perScore > storeScores.bronceHighScores[i-1]) {
                    storeScores.bronceHighScores[i-1] = perScore;
                    storeScores.bronceHighSeconds[i-1] = gameTimer.timer;
                    SaveManager.instance.activeSave.bronceHighScoresSave[i-1] = perScore;
                    SaveManager.instance.activeSave.bronceHighSecondsSave[i-1] = gameTimer.timer;
                }
            }
            if (perScore >= 0.75 && perScore < 1) {
                if (perScore > storeScores.silverHighScores[i-1]) {
                    storeScores.silverHighScores[i-1] = perScore;
                    storeScores.silverHighSeconds[i-1] = gameTimer.timer;
                    SaveManager.instance.activeSave.silverHighScoresSave[i-1] = perScore;
                    SaveManager.instance.activeSave.silverHighSecondsSave[i-1] =  gameTimer.timer;
                }
            }
            if (perScore >= 1) {
                if (perScore > storeScores.goldHighScores[i-1]) {
                    storeScores.goldHighScores[i-1] = perScore;
                    storeScores.goldHighSeconds[i-1] = gameTimer.timer;
                    SaveManager.instance.activeSave.goldHighScoresSave[i-1] = perScore;
                    SaveManager.instance.activeSave.goldHighSecondsSave[i-1] = gameTimer.timer;
                }
            }
        //}
        SaveManager.instance.activeSave.secretsFound[i - 1] = secretValue;

        SaveManager.instance.SaveBin();
        SaveUI.instance.SaveBinInfo();
    }

    void scorebarFiller()
    {
        scoreBar.fillAmount = runningScore / 100;
        scoreBar.color = scorebarColor.Evaluate(runningScore / 100);
    }

    public void TimeScaleIsOne()
    {
        Time.timeScale = 1f;
    }



}// class
