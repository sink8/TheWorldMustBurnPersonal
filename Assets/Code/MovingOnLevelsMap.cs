using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingOnLevelsMap : MonoBehaviour
{

    [Header("Destinations")]
    public GameObject upDestination;
    public GameObject downDestination;
    public GameObject leftDestination;
    public GameObject rightDestination;

    [Header("Final Destinations")]
    public GameObject upDestinationFinal;
    public GameObject downDestinationFinal;
    public GameObject leftDestinationFinal;
    public GameObject rightDestinationFinal;


    [Header("Stuff")]
    public GameObject player;
    private bool canMove;
    public bool locked;

    [SerializeField] public bool currentLevel;

    public string levelName;
    public int levelNumber;
    public string levelCode;

    public LevelSelector levelSelector;
    public MenuNavigation menuNav;
    CloudsSpawn spawn;
    StoreScores storeScores;

    private void Awake() {
        //storeScores = GetComponent<StoreScores>();
        spawn = FindObjectOfType<CloudsSpawn>();
    }

    // Update is called once per frame
    void Update()
    {

        if(player.transform.position == transform.position) {
            currentLevel = true;
        }

        if(currentLevel == true && locked == false) {
            // press something and level loads
        }

        if ((Input.GetAxis("Vertical") > 0)) {
            if (upDestination != null && upDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(upDestination));
            }
        } else if (Input.GetAxis("Vertical") < 0) {
            if (downDestination != null && downDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(downDestination));
            }
        } else if (Input.GetAxis("Horizontal") > 0) {
            if (rightDestination != null && rightDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(rightDestination));
            }
        } else if (Input.GetAxis("Horizontal") < 0) {
            if (leftDestination != null && leftDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(leftDestination));
            }
        } 

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            if (upDestination != null && upDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ) {
            if (downDestination != null && downDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            if (rightDestination != null && rightDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (leftDestination != null && leftDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            
            SaveManager.instance.activeSave.respawnPosition[0] = player.transform.position.x;
            SaveManager.instance.activeSave.respawnPosition[1] = player.transform.position.y;
            SaveManager.instance.activeSave.respawnPosition[2] = player.transform.position.z;

            spawn.DeleteCloud();

            SelectLevel(levelNumber);

            //if (currentLevel == true && levelNumber == 1) {
            //    levelSelector.LoadLevel1();
            //}
            //if (currentLevel == true && levelNumber == 2) {
            //    levelSelector.LoadLevel2();
            //}
            //if (currentLevel == true && levelNumber == 3) {
            //    levelSelector.LoadLevel3();
            //}
        }
    }

    IEnumerator Move( GameObject direction ) {
        yield return new WaitForSeconds(1 / 60);
        while (player.transform.position != direction.transform.position) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, direction.transform.position, 3f * Time.deltaTime);
            yield return null;
        }
    }

    private void SelectLevel(int level) {
        if(currentLevel == true && levelNumber == level && locked == false) {
            menuNav.CloseLevelMenu();
            menuNav.CloseMixedMenuStuff();
            
            for(int i = 0; i < levelSelector.levelsAvailable.Length; i++) {
                if(level == i + 1) {
                    levelSelector.LoadLevels(i);
                }
            }

            //if(i == 1) {
            //    levelSelector.LoadLevel1();
            //}
            //if(i == 2) {
            //    levelSelector.LoadLevel2();
            //}
            //if(i == 3) {
            //    levelSelector.LoadLevel3();
            //}
        }
    }

  
} // class
