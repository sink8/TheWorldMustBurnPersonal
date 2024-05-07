using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsForPlayer : MonoBehaviour
{

    public GameObject textgo;
    public Canvas canvas;
    //bool jumpbool, timerbool = false;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            canvas.enabled = true;
            textgo.SetActive(true);
        }

    }
    //private void OnTriggerStay2D(Collider2D other) {
    //        if (other.gameObject.CompareTag("Player")) {
    //        canvas.enabled = true;
    //        textgo.SetActive(true);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision) {
        canvas.enabled = false;
        textgo.SetActive(false);
    }



}// class
