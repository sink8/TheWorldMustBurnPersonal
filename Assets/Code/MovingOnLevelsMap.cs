using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MovingOnLevelsMap : MonoBehaviour
{
    Newcontrolsmap _inputActions;
    Vector2 movement;
    bool _pressed = false;
    public float hor = 0;
    public float ver = 0;

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
    [SerializeField] StoreScores storeScores;

    private void OnEnable()
    {
        _inputActions = new Newcontrolsmap();
        _inputActions.map.Enable();
        //_inputActions.map.Press.started += ctx => _pressed = true;
        _inputActions.map.Press.started += OnPress;
    }

    private void OnDisable()
    {
        _inputActions.map.Disable();
        _inputActions.map.Press.started -= OnPress;
        //_inputActions.map.Press.performed += ctx => _pressed = false;
    }
    private void Awake() {
        storeScores = GetComponentInParent<StoreScores>();
        
        spawn = FindObjectOfType<CloudsSpawn>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        hor = movement.x;
        ver = movement.y;

        if (player.transform.position == transform.position) {
            currentLevel = true;
        }

        if(currentLevel == true && locked == false) {
            // press something and level loads
            storeScores.levelNameX = levelName;
        }

        if ((Input.GetAxis("Vertical") > 0) || ver > 0.6f) {
            if (upDestination != null && upDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(upDestination));
            }
        } else if ((Input.GetAxis("Vertical") < 0) || ver < -0.6f ) {
            if (downDestination != null && downDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(downDestination));
            }
        } else if ((Input.GetAxis("Horizontal") > 0)|| hor > 0.6f) {
            if (rightDestination != null && rightDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(rightDestination));
            }
        } else if ((Input.GetAxis("Horizontal") < 0) || hor < -0.6f) {
            if (leftDestination != null && leftDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == false) {
                currentLevel = false;
                StartCoroutine(Move(leftDestination));
            }
        } 

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || ver > 0.6f) {
            if (upDestination != null && upDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || ver < -0.6f ) {
            if (downDestination != null && downDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || hor > 0.6f) {
            if (rightDestination != null && rightDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || hor < -0.6f) {
            if (leftDestination != null && leftDestinationFinal.GetComponent<MovingOnLevelsMap>().locked == true) {
                AudioFW.Play("MenuCan'tGoOn");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) ) {
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || _pressed == true) {
            
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

    private void OnPress(InputAction.CallbackContext context)
    {
        SaveManager.instance.activeSave.respawnPosition[0] = player.transform.position.x;
        SaveManager.instance.activeSave.respawnPosition[1] = player.transform.position.y;
        SaveManager.instance.activeSave.respawnPosition[2] = player.transform.position.z;

        spawn.DeleteCloud();

        SelectLevel(levelNumber);
    }

    void HandleInput()
    {
        movement = _inputActions.map.Move.ReadValue<Vector2>();
        
        //Debug.Log(movement.ToString());
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
