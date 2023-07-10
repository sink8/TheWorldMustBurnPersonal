using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{

    [SerializeField]
    [TextArea]
    List<string> dialogueLines;
    int lineIndex;

    TMP_Text text;
    CanvasGroup group;
    bool dialogueStarted;

    public NPCDialogueTrigger trigger;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (!dialogueStarted) {
                lineIndex = 0;
                text.SetText(dialogueLines[lineIndex]);
                group.alpha = 1;
                dialogueStarted = true;
                //} else if (lineIndex < dialogueLines.Count) {
                //    text.SetText(dialogueLines[lineIndex++]); //  
                //}
                //else {
                //    group.alpha = 0;
                //}
            }

        }
    }

    void StartDialogue() {
        if (!dialogueStarted) {
            lineIndex = 0;
            text.SetText(dialogueLines[lineIndex]);
            group.alpha = 1;
            dialogueStarted = true;
            //} else if (lineIndex < dialogueLines.Count) {
            //    text.SetText(dialogueLines[lineIndex++]); //  
            //}
            //else {
            //    group.alpha = 0;
            //}
        }
    }

    void Update()
    {
        if(trigger.triggered == true) {
            StartDialogue();
        }
    }
}
