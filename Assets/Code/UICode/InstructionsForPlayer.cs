using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InstructionsForPlayer : MonoBehaviour
{

    public GameObject textgo;
    public Canvas canvas;

    public string jump;
    public string dash;
    public string dashDown;
    public string shoot;

    public string jumpPad, dashPad, dashDownPad, shootPad;

    public TMPro.TextMeshProUGUI jumpTmp;
    public TMPro.TextMeshProUGUI dashTmp;
    public TMPro.TextMeshProUGUI shootTmp;
    public TMPro.TextMeshProUGUI dashDownTmp;

     public TMPro.TextMeshProUGUI jumpTmpPad, dashTmpPad, shootTmpPad, dashDownTmpPad;

    public TMPro.TextMeshProUGUI textCanv;

    public GameObject objtjump, objtdash, objtdashDown, objShoot;
    public GameObject objtjumpPad, objtdashPad, objtdashDownPad, objShootPad;

    bool usingController = false;

    public int triggernum = 10;
    // 1 jump, 2 jump2, 3 dash, 4 water, 5 shoot
    private void Start()
    {
        
        //objtjump = GameObject.Find("ActionBindingTextJump");
        //jumpTmp = objtjump.GetComponent<TMPro.TextMeshProUGUI>();

        // objtdash = GameObject.Find("ActionBindingTextDash");
        // dashTmp = objtdash.GetComponent<TMPro.TextMeshProUGUI>();

        // objtdashDown = GameObject.Find("ActionBindingTextDashDown");
        // dashDownTmp = objtdashDown.GetComponent<TMPro.TextMeshProUGUI>();

        // objShoot = GameObject.Find("ActionBindingTextShoot");
        // shootTmp = objShoot.GetComponent<TMPro.TextMeshProUGUI>();


        // objtjumpPad = GameObject.Find("ActionBindingTextJumpPad");
        // jumpTmpPad = objtjumpPad.GetComponent<TMPro.TextMeshProUGUI>();

        // objtdashPad = GameObject.Find("ActionBindingTextDashPad");
        // dashTmpPad = objtdashPad.GetComponent<TMPro.TextMeshProUGUI>();

        // objtdashDownPad = GameObject.Find("ActionBindingTextDashDownPad");
        // dashDownTmpPad = objtdashDownPad.GetComponent<TMPro.TextMeshProUGUI>();

        // objShootPad = GameObject.Find("ActionBindingTextShootPad");
        // shootTmpPad = objShootPad.GetComponent<TMPro.TextMeshProUGUI>();

        //var actionBindTransform = objt.transform.Find("ActionBindingText");

        //if (actionBindTransform != null)
        //{
        //    objt2 = actionBindTransform.gameObject;
        //    // Now you can use actionBindingText as needed
        //}
    }
    //bool jumpbool, timerbool = false;


    // private void OnTriggerEnter2D(Collider2D collision) {
    //     if (collision.gameObject.CompareTag("Player")) {
    //         canvas.enabled = true;
    //         textgo.SetActive(true);
    //     }

    // }

    private void Update()
    {
        jump = jumpTmp.text;
        dash = dashTmp.text;
        dashDown = dashDownTmp.text;
        shoot = shootTmp.text;
        jumpPad = jumpTmpPad.text;
        dashPad = dashTmpPad.text;
        dashDownPad = dashDownTmpPad.text;
        shootPad = shootTmpPad.text;

        // Ei tunnista pag komentoa koska pad näppäimet eivät valittuna kun ohjain ei ole kytketty. 

        //if (Input.GetJoystickNames().Length > 0)


            if (Gamepad.all.Count > 0)
            {
                usingController = true;
            }
            else
            {
                usingController = false;
            }
        //Instuctions();
    }
    private void OnTriggerStay2D(Collider2D other) {
           if (other.gameObject.CompareTag("Player")) {
           // Debug.Log($"Triggernum set to {triggernum} for {gameObject.name}");
            Instuctions();
               canvas.enabled = true;
               textgo.SetActive(true);
       }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        canvas.enabled = false;
        textgo.SetActive(false);
    }

    void Instuctions()
    {
        //Debug.Log($"Triggernum before switch: {triggernum}, Object: {gameObject.name}");
        
        switch (triggernum)
        {
            
            case 7:
                print(" ");
                textCanv.text = "Checkpoint";
                break;
            case 6:
                print("Press " + dashDown );
                textCanv.text = "Press  " + dashDown;
                break;
            case 5:
                print("Water is dangerous");

                break;
            case 4:
                
                if (usingController == false)
                {
                    
                    textCanv.text = "Press  " + " + direction to shoot fireballs";
                }
                else
                {
                    textCanv.text = "Press " + shootPad + " + direction to shoot fireballs";

                }

                break;
            case 3:
                
                if (usingController == false)
                {
                    textCanv.text = "Press  " + dash + " after tree has burned";
                }
                else
                {
                    textCanv.text = "Press " + dashPad + " after trees have burned";
                    
                }

                break;

            case 2:
                
                if (usingController == false)
                {
                    textCanv.text = "Press  " + jump + " 2 times";
                }
                else
                {
                    textCanv.text = "Press " + jumpPad + " 2 times ";
                    //print("case 2");
                }
                break;

            case 1:
                if (usingController == false)
                {
                    textCanv.text = "Press  " + jump;
                    print("case 1");
                }
                else
                {
                    textCanv.text = "Press  " + jumpPad;
                }

                    break;
                
            default:
                
                textCanv.text = " _ " ;
                break;
        }
    }

}// class
