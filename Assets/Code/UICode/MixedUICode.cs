using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MixedUICode : MonoBehaviour
{
   // [SerializeField] Text credits;
    public float creditsSpeed = 1f;
    public float creditsTime = 20f;
    float timer;
    public TMP_Text credits; 
    [SerializeField] GameObject creditsObject;
    public Vector3 startingPosition;
    [SerializeField] MenuNavigation menuNav;
    bool rolling = false;
    bool creditsCodeRunning = false;
    public RectTransform rec;

    public TMP_Text buttonText;
    public TMP_Text DashText;
    public TMP_Text ShootText;
    public TMP_Text buttonText2;
    GameManager gm;
    bool waitingForKey = false;
    public Transform optionsPanel;
    public InputManager inputManager;
    Event keyEvent;
    KeyCode newKey;
    [SerializeField] KeyBinding keyBinding;
    [SerializeField] KeybindingActions keyBindingA;

    private void Awake() {

        //inputManager = InputManager.instance;
        startingPosition = creditsObject.transform.localPosition;
        timer = creditsTime;
        optionsPanel = transform.Find("options");
        Credits();
        //ChangeKey();
    }

 
    void Update()
    {
        if(creditsCodeRunning == true){
        CreditsScroll();
        }
    }

    void Credits(){
        credits.text = "Developer / Art, Code and Desing Sini Karhunen  \n" +
            "\n Audio: " +
            "\n Sound Effects: freesound.org/people/YleArkisto/" +
            "\n Sound Effects: pixabay.com/" +
            " \n" +
            "\n Music: " +
            "\n Tracks of levels 4, 5 and 7 by NjilNjil / Jade" +
            "\n  " +
            "\n Other music by Eric Matyas  www.soundimage.org" + 
            "\n Alley-Grunge, Fantasy Forest Battle, Forest Chase, Fantasy World Menu 2, A Flock of Bubbles Looping  " +
            "\n Battle of the Ancients, Dark lands,Night Things,Exotinc Dreaming, Ice in the Sky, Magical Gateway Looping, Mystical Journey Looping  " +
            "\n Night Flight, Over Ancient Waters, Skies Are Clearing, Stratosphere Looping, Windle Pixel Seaside  Adventure " +
            "\n " +
            "\n Kewin MacLeod incompetech.com/music/royalty-free/music.html " +
            "\n Ancient Winds, Aquarium, Bleeping Demo, Ethearal Relaxation, Ethearal Club, Journey To Ascend, Magic Escape Room, Mesmerizing Galazy" +
            "\n Pleasant Porridge, Voxel Revolution, Wholesome" +
            "\n " +
            "\n Special thanks " +
            "\n Friends and family" +
            "\n Ippa" +
            "\n Sade ja Viima" +
            "\n Gametesters, especially" +
            "\n Borb" +
            "\n Tiine Yrjönsalo" +
            "\n Ida Pihlajamaa" +
            "\n Aleksi (RiceMunk) Suutarinen" +
            "\n Mansku" +
            "\n Minna and Sami"+
            "\n Aki Kanerva" +
            "\n All LGIN Finland testesrs" +
            "\n IGDA Finland Helsinki testers" +
            "\n" +
            "\n LGIN community, mentors and Suvi Kiviniemi" +
            "\n ANd last but not least, Laajasalon Opisto Game Design. Year 2020-2021 and teachers Yrjö Peussa and Andrei Rodriguez and all the students who tested over the years" +
            "\n and all those that I forgot";

    }

    public void CreditsScroll(){


        if(timer > 0 && rolling == true){
            timer -= Time.deltaTime;
            rec.transform.Translate(Vector3.up * Time.deltaTime * creditsSpeed);

        }else {
            // suljetaan hommeli
            credits.transform.localPosition = startingPosition;

            menuNav.CloseCreditsMenu();
            menuNav.OpenTitleMenu();
            timer = creditsTime;
            rolling = false;
            creditsCodeRunning = false;
        }
    }

    public void CreditsRolling(){
        rolling = true;
        creditsCodeRunning = true;
    }

    public void CloseCredits(){
        credits.transform.localPosition = startingPosition;
        timer = creditsTime;
        rolling = false;
        creditsCodeRunning = false;
    }

    public void ChangeKey(){
        // for(int i = 0; i<5; i++){
        //         if(optionsPanel.GetChild(i).name == "JumpKey"){
        //             optionsPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = inputManager.GetKeyForAction(KeybindingActions.Jump).ToString();
        //         }
        // }
        buttonText.text = inputManager.GetKeyForAction(KeybindingActions.Jump).ToString();
        DashText.text = inputManager.GetKeyForAction(KeybindingActions.Dash).ToString();
        ShootText.text = inputManager.GetKeyForAction(KeybindingActions.Shoot).ToString();
    }

    private void OnGUI() {
        keyEvent = Event.current;
        if(keyEvent.isKey && waitingForKey) {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }

    }

    public void StartAssignment(string keyName){
        if(!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(TMP_Text text) {
        buttonText2 = text;
    }
    
    IEnumerator WaitForKey() {
        while (keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName) {
        waitingForKey = true;

        yield return WaitForKey();

        switch (keyName) {
            case "jump":
                print("joo");
                keyBinding.keybindingChecks[0].keyCode = newKey;
                buttonText2.text = inputManager.GetKeyForAction(KeybindingActions.Jump).ToString();
                break;

        }
        switch (keyName) {
            case "dash":
                print("joo");
                keyBinding.keybindingChecks[1].keyCode = newKey;
                buttonText2.text = inputManager.GetKeyForAction(KeybindingActions.Dash).ToString();
                break;

        }


    }

    public bool ChangeKeyBinding(KeybindingActions key) {
        foreach (KeyBinding.KeybindingCheck keybindingCheck in keyBinding.keybindingChecks) {
            if (keybindingCheck.keybindingAction == key) {
                return Input.GetKeyDown(keybindingCheck.keyCode);
            }
        }

        return false;
    }

}
