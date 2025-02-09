using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
    public GameObject levelMenu, deathMenu, levelEndMenu, pauseMenu, mixMenuStuff, TitleMunu, SavesMenu, StartSavesMenu, CreditsMenu, OptionsMenu, FirstCanvas, AudioMenu, OptionsMenuGameplay, intro;
    public GameObject levelFirstButton, levelEndFirstButton, DeathFirstButton, PauseFirstButton, TitleFirstButton, SaveFirstButton, StartFirstButton, OptionsFirstButton,
                        CreditsFirstButton, OptionsFirstButtonGameplay, KeyPindingsFisrt, PadPindingsFirst, SaveExistsFirst, IntroFirst;

    public GameObject lv1Score, lv2Score, lv3Score;

    public CanvasGroup closeLevelC, deathC;
    public float fadingSpeed = 0.5f;
    public bool fadeboolLevel, fadeboolDeath = false;

    public GameObject player;
    public bool isPlayerMoving = false;
    public Vector3 lastPosition;

    public GameObject[] levels;
    public GameObject[] locks;
    public MenuAudio menuAudio;
    public bool pauseOpen = false;
    public bool canYouOpenPauseMenu = false;
    bool gameStarted = false;

    public Button backButton;
    public int randomInt;

    Newcontrolsmap _inputActions;
    public PlayerInput _playerInput;   
    InputAction _menuActions;

    public GameObject nodes;
    InputAction cancelAction;


    private void OnEnable()
    {
        _inputActions = new Newcontrolsmap();
        _inputActions.map.Enable();
        _inputActions.map.Pause.started += OnPause;
        _inputActions.map.Cancel.performed += CancelTest;

        //_inputActions.map.Pause.started += ctx => _pressed = true;
        //_inputActions.map.Pause.canceled += ctx => _pressed = false;
    }

    private void OnDisable()
    {
        _inputActions.map.Pause.started -= OnPause;
        _inputActions.map.Cancel.performed -= CancelTest;
        _inputActions.map.Disable();

    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (canYouOpenPauseMenu)
        {
            if (pauseOpen)
            {
                ClosePauseMenu();
                PauseOpenFalse();
            }
            else
            {
                OpenPauseMenu();
                pauseOpen = true;
            }
            AudioFW.Play("MenuEnd");
        }
    }

    private void Start() {
        //levels = GameObject.FindGameObjectsWithTag("Level");
        //locks = GameObject.FindGameObjectsWithTag("Locks");
        lastPosition = player.transform.position;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(TitleFirstButton);

        //_menuActions = _playerInput.actions["Pause"];

    }

    private void Update() {

        IsPlayerMoving();
        /*if(player.transform.position == levels[0].transform.position) {
            lv1Score.SetActive(true);
            lv2Score.SetActive(false);
            lv3Score.SetActive(false);
        } else if(player.transform.position == levels[1].transform.position) {
            lv1Score.SetActive(false);
            lv2Score.SetActive(true);
            lv3Score.SetActive(false);
        } else if (player.transform.position == levels[2].transform.position) {
            lv1Score.SetActive(false);
            lv2Score.SetActive(false);
            lv3Score.SetActive(true);
        }*/

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnlockAllLevels();
        }

        if (canYouOpenPauseMenu == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TriggerBackButton();
            }

        }


        //if(isPlayerMoving == false)
        //{

            //if (canYouOpenPauseMenu == true) {
            //    if(pauseOpen == false)
            //    {
            //        if (Input.GetKeyDown(KeyCode.Escape))
            //        {
            //            OpenPauseMenu();
            //            pauseOpen = true;
            //            AudioFW.Play("MenuEnd");
            //        }

            //        //if (_pressed == true)
            //        //{
            //        //    OpenPauseMenu();
            //        //    pauseOpen = true;
            //        //    AudioFW.Play("MenuEnd");
            //        //}
            //    }
            //    else if (pauseOpen == true) {
            //        if (Input.GetKeyDown(KeyCode.Escape))
            //        {
            //            ClosePauseMenu();
            //            PauseOpenFalse();
            //            AudioFW.Play("MenuEnd");

            //        }

            //    }

            //}
       // }

        
        

        /*if (EventSystem.current.currentSelectedGameObject != null) {
        if(EventSystem.current.currentSelectedGameObject.name == "Level1Button") {
            lv1Score.SetActive(true);
            lv2Score.SetActive(false);
            lv3Score.SetActive(false);
        } else if (EventSystem.current.currentSelectedGameObject.name == "Level2Button") {
            lv1Score.SetActive(false);
            lv2Score.SetActive(true);
            lv3Score.SetActive(false);
        } else if (EventSystem.current.currentSelectedGameObject.name == "Level3Button") {
            lv1Score.SetActive(false);
            lv2Score.SetActive(false);
            lv3Score.SetActive(true);
        }
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }*/
    }

    public void IsPlayerMoving()
    {
        if (player == null) return;

        // Check if the player's position has changed
        if (player.transform.position != lastPosition)
        {
            if (!isPlayerMoving)
            {
                isPlayerMoving = true;
                Debug.Log($"Player started moving from {lastPosition} to {player.transform.position}");
            }
        }
        else
        {
            if (isPlayerMoving)
            {
                isPlayerMoving = false;
                Debug.Log($"Player stopped moving at {player.transform.position}");
            }
        }

        // Update last position for the next frame
        lastPosition = player.transform.position;
    }


    public void PauseOpenTrue()
    {
        pauseOpen = true;
    }

    public void PauseOpenFalse()
    {
        pauseOpen = false;
    }

    void CancelTest(InputAction.CallbackContext ctx)
    {
        //Debug.Log("cancel test");
        if (canYouOpenPauseMenu == false)
        {
                TriggerBackButton();
        }
    }

    public void TriggerBackButton() {
        if (backButton != null) {
            backButton.onClick.Invoke();
        }
    }

    public void SetBackButton(Button newBackButton) {
        backButton = newBackButton;
        Debug.Log("Back button updated to: " + newBackButton.name);
    }





    void OpenPauseHere()
    {
        OpenPauseMenu();
        pauseOpen = true;
        AudioFW.Play("MenuEnd");
       
        
    }

    void ClosePauseHere()
    {
        ClosePauseMenu();
        
        AudioFW.Play("MenuEnd");
        //nodes.SetActive(true);
        
    }
 
    public void OpenLevelMenu() {
        menuAudio.StopMenuMusic();
        levelMenu.SetActive(true);
        _inputActions.UI.Enable();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelFirstButton);
    }

    public void CloseLevelMenu() {
        levelMenu.SetActive(false);
        _inputActions.UI.Disable();
    }

    public void OpenLevelEndMenu() {
            levelEndMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelEndFirstButton);
    }

    public void CloseLevelEndMenu() {
        levelEndMenu.SetActive(false);
    }

    public void OpenDeathMenu() {
        deathMenu.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(DeathFirstButton);
        
    }
    public void CloseDeathMenu() {
        deathMenu.SetActive(false);
    }

    public void OpenPauseMenu() {
        nodes.SetActive(false);
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PauseFirstButton);
    }

    public void OpenSaveExists()
    {
        EventSystem.current.SetSelectedGameObject(SaveExistsFirst);
    }

    public void ClosePauseMenu() {
        pauseMenu.SetActive(false);
        //pauseOpen = false;
        Time.timeScale = 1f;
        nodes.SetActive(true);
        Debug.Log("Time.timeScale is now: " + Time.timeScale);
    }


    public void FadeDeathPanel() {
        //var CanvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DoFade(deathC, deathC.alpha, fadeboolDeath ? 1 : 0));
        fadeboolDeath = !fadeboolDeath;

    }

    public void FadeLevelEnd() {
        StartCoroutine(DoFade(closeLevelC, closeLevelC.alpha, fadeboolLevel ? 1 : 0));
        fadeboolLevel = !fadeboolLevel;

    }

    public void CloseMixedMenuStuff() {
        mixMenuStuff.SetActive(false);
    }

    public void OpenMixedMenuStuff() {
        mixMenuStuff.SetActive(true);
        gameStarted = true;
        player.transform.position = new Vector3( SaveManager.instance.activeSave.respawnPosition[0], SaveManager.instance.activeSave.respawnPosition[1], SaveManager.instance.activeSave.respawnPosition[2]);
        canYouOpenPauseMenu = true;

    }

    IEnumerator MixedMenuDelay()
    {
        yield return new WaitForSeconds(25);
        OpenMixedMenuStuff();
        menuAudio.StopMenuMusic();
        levelMenu.SetActive(true);
        _inputActions.UI.Enable();
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(levelFirstButton);
        StopIntro();
        
    }

    public void OpenMixedMenuStuffDelay()
    {
        StartCoroutine(MixedMenuDelay());

    }

    public void CloseTitleMenu() {
        TitleMunu.SetActive(false);
    }

    public void OpenTitleMenu() {
        if(canYouOpenPauseMenu == false)
        {
            TitleMunu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(TitleFirstButton);

        }
    }

    public void GoBackToTitleMenu()
    {

            TitleMunu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(TitleFirstButton);

    }

    public void CloseSavesMenu() {
        SavesMenu.SetActive(false);
    }

    public void CloseFirstMenu() {
        StartCoroutine(waiter());
        //FirstCanvas.SetActive(false);
    }

    IEnumerator waiter() {
        yield return new WaitForSeconds(3);
        FirstCanvas.SetActive(false);
    }

    public void OpenSavesMenu() {
        SavesMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SaveFirstButton);
    }

    public void CloseStartSavesMenu() {
        StartSavesMenu.SetActive(false);
    }

    public void OpenStartSavesMenu() {
        StartSavesMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(StartFirstButton);
    }

    public void CloseCreditsMenu() {
        CreditsMenu.SetActive(false);
    }

    public void CloseOptionsMenu() {
        if (gameStarted)
        {
            OpenPauseMenu();    
        }else
        {
            OpenTitleMenu();
        }


        OptionsMenu.SetActive(false);
    }

    public void CloseOptionsMenuGameplay()
    {
        OptionsMenuGameplay.SetActive(false);
    }


    public void OpenCreditsMenu() {
        CreditsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CreditsFirstButton);
    }

    public void OpenOptionsMenu() {
        OptionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
    }

    public void OpenOptionsMenuGameplay()
    {
        OptionsMenuGameplay.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsFirstButtonGameplay);
    }

    public void OpenOptionsKeyBindings() {
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(KeyPindingsFisrt);
    }

    public void OpenOptionsKeyBindingsPad() {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(PadPindingsFirst);
    }

    public void OpenMenuNoise()
    {
        AudioMenu.SetActive(true);
        //AudioFW.Play("MenuClick");
    }
    public void CloseMenuNoise()
    {
        AudioMenu.SetActive(false);
        AudioFW.Play("MenuEnd");
    }

    public void CanYouOpenPauseMenuYes()
    {
        canYouOpenPauseMenu = true;
    }

    public void CanYouOpenPauseMenuNo()
    {
        canYouOpenPauseMenu = false;
    }

    public void PlayIntro()
    {
        intro.SetActive(true);
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(IntroFirst);
    }

    public void StopIntro()
    {
        StopAllCoroutines();
        intro.SetActive(false);
        levelMenu.SetActive(true);
        _inputActions.UI.Enable();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelFirstButton);
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end) {
        float counter = 0f;

        while(counter < fadingSpeed) {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadingSpeed);

            yield return null;
        }
    }

    public void UnlockAllLevels() {
        print("unlock key pressed");
        for (int j = 0; j < locks.Length; j++) {
            locks[j].SetActive(false);
        }

        for (int i= 0; i < levels.Length; i++) {
            levels[i].GetComponent<MovingOnLevelsMap>().locked = false;

        }
    }

} // class
