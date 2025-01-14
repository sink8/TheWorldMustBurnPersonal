using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelEnd : MonoBehaviour
{
    public int LevelNumber = 1;

    [SerializeField] int tileAmount;
    public FireManager fm;
    public StoreScores storeScores;
    public GameObject storeSGM;
    public GameTimer gameTimer;
    public ScoreCounter scoreCounter;
    public Text EndLevelScoreTextCommon;
    public Text EndLevelScoreTextHighScore;
    public GameObject endLevelQuestion;

    GameManager gm;

    public MenuNavigation menuNav;
    public GameObject pause;
    public PlayLoops playLoops;
    public Animator anim;
    public Image imag;
    bool levelend = false;
    public bool pauseOpen = false;
    float timer = 2f;

    public GameObject[] playerParticles;
    public InputManager inputManager;

    public GameObject thisLevel;

    //public Transform secret1Position, secret2Position;


    void Start()
    {
        inputManager = InputManager.instance;
        playLoops = FindObjectOfType<PlayLoops>();
        playLoops.StartLevelMusic(LevelNumber);
        

        tileAmount = fm.GetComponent<FireManager>().GetTileAmountSprite();

        menuNav = FindObjectOfType<MenuNavigation>(); 
        gm = FindObjectOfType<GameManager>();
        scoreCounter = FindObjectOfType<ScoreCounter>();
        gameTimer = FindObjectOfType<GameTimer>();
        storeScores = FindObjectOfType<StoreScores>();
        // playLoops = FindObjectOfType<PlayLoops>();

        if (playerParticles == null)
            playerParticles = GameObject.FindGameObjectsWithTag("Particles");


    }

    private void Update() {
        if (levelend == true) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    timer = 0;
                    menuNav.FadeLevelEnd();
                }
            }
        }

        //if (pauseOpen == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        //playLoops.StopLevelMusic();
        //        //anim.Play("FadeOut");
        //        pause.SetActive(!pause.activeSelf);
        //        //menuNav.OpenPauseMenu();
        //        AudioFW.Play("MenuClick");
        //        pauseOpen = true;
        //        //Destroy(transform.parent.gameObject, 3f);

        //    }
        //}
            
    //    if (pauseOpen == true)
    //        {
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //                //playLoops.StopLevelMusic();
    //                //anim.Play("FadeOut");
    //                menuNav.ClosePauseMenu();
    //                AudioFW.Play("MenuEnd");
    //                //pauseOpen = false;

    //        }
            
    //}

        //if (pauseOpen == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.T))
        //    {
        //        //playLoops.StopLevelMusic();
        //        //anim.Play("FadeOut");
        //        menuNav.ClosePauseMenu();
        //        AudioFW.Play("MenuEnd");
        //        //pauseOpen = false;

        //    }
        //}
    }

    public void PauseOpenBool()
    {
        pauseOpen = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            endLevelQuestion.SetActive(true);
                

            if (Input.GetKeyDown(KeyCode.G) || (UserInput.instance.ContinueInput))
            {
                Debug.Log(" trigger presses end");
                levelend = true;
                LevelEndParticles();
                playLoops.StopLevelMusic();
                SaveManager.instance.SaveBin();
                menuNav.OpenLevelEndMenu();
                EndLevelScoreTextCommon = GameObject.Find("CommonEndText").GetComponent<Text>();
                EndLevelScoreTextHighScore = GameObject.Find("HighScoreEndText").GetComponent<Text>();
                LevelEndTextCommon();
                scoreCounter.RegisterNewScore(LevelNumber);
                SecretManager.Instance.SaveSecrets();
                SecretManager.Instance.GetTotalFoundSecrets();
                //scoreCounter.RegisterScore();
                // anim.Play("FadeOut");
                //scoreCounter.scoreValue = 0;
                //Destroy(gameObject,3f);
                Destroy(collision.transform.parent.gameObject, 2f);
                Destroy(thisLevel, 2f);
            }

        }


    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Player")) {
    //        endLevelQuestion.SetActive(true);
    //        Debug.Log(" trigger enter");
    //        if (Input.GetKeyDown(KeyCode.G) || (UserInput.instance.ContinueInput))
    //        {
                
    //            levelend = true;
    //            LevelEndParticles();
    //            playLoops.StopLevelMusic();
    //            SaveManager.instance.SaveBin();
    //            menuNav.OpenLevelEndMenu();
    //            EndLevelScoreTextCommon = GameObject.Find("CommonEndText").GetComponent<Text>();
    //            EndLevelScoreTextHighScore = GameObject.Find("HighScoreEndText").GetComponent<Text>();
    //            LevelEndTextCommon();
    //            scoreCounter.RegisterNewScore(LevelNumber);
    //            SecretManager.Instance.SaveSecrets();
    //            SecretManager.Instance.GetTotalFoundSecrets();
    //            //scoreCounter.scoreValue = 0;
    //            //Destroy(gameObject,3f);
    //            Destroy(collision.transform.parent.gameObject, 2f);
    //            Destroy(thisLevel, 2f);
    //        }


    //    }
    //}
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        endLevelQuestion.SetActive(false);
    }

    void LevelEndTextCommon() {
        if(scoreCounter.runningScore < 50 && scoreCounter.runningScore > 0) {
            EndLevelScoreTextCommon.text = "Not Much was Burned..."; 
        } else if (scoreCounter.runningScore == 0) {
            EndLevelScoreTextCommon.text = "Concrats! Didn't burn anything at all";
        } else if (scoreCounter.runningScore >= 50 && scoreCounter.runningScore < 75) {
            EndLevelScoreTextCommon.text = "You burned something alright " + scoreCounter.runningScore + " %";
        } else if (scoreCounter.runningScore >= 75 && scoreCounter.runningScore < 100) {
            EndLevelScoreTextCommon.text = "Almost there, but not yet " + scoreCounter.runningScore + " %";
        } else if (scoreCounter.runningScore == 100 ) {
            EndLevelScoreTextCommon.text = "100 % !!";
        } else if (scoreCounter.runningScore > 100) {
            EndLevelScoreTextCommon.text = "You burned more than 100 %, how is that possible? " + scoreCounter.runningScore + " %";
        }


        //for(int i = 0; i < storeScores.levels.Length; i++) {
        //    if(LevelNumber == i + 1) {
        //        if (((scoreCounter.runningScore >= 50 && scoreCounter.runningScore < 75) && (scoreCounter.runningScore > storeScores.bronceHighScores[i] * 100))) {
        //            EndLevelScoreTextHighScore.text = "New highscore  for  bronze  category";
        //        } else if (((scoreCounter.runningScore >= 75 && scoreCounter.runningScore < 100) && (scoreCounter.runningScore > (storeScores.silverHighScores[i] * 100)))) {
        //            EndLevelScoreTextHighScore.text = "New highscore for  the  Silver  category";
        //        } else if ((scoreCounter.runningScore >= 100 && (scoreCounter.runningScore > (storeScores.goldHighScores[i] * 100)))) {
        //            EndLevelScoreTextHighScore.text = "New highscore for the Gold category";
        //        } else EndLevelScoreTextHighScore.text = "";
        //    }
        //}
    }

    public void LevelEndParticles() {

         
                playerParticles = GameObject.FindGameObjectsWithTag("Particles");

        foreach (GameObject part in playerParticles) {
            var particle = part.GetComponent<ParticleSystem>();
            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        //for (int i = 0; i < playerParticles.Length; i++) {
        //    playerParticles[i]
        //}

        }


}
