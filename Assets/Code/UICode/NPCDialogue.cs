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
    public EnemyHealth enemyHealth;
    bool dialogueStarted;
    [SerializeField] bool isCloud = false;

    public NPCDialogueTrigger trigger;
    public float timeTillChange = 4f;
    float timer = 0;

    public bool isPlant, isWater, isPilvi, isRock;
    public Animator anim;
    public bool hasHealth = true;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        group = GetComponent<CanvasGroup>();
        enemyHealth = gameObject.GetComponentInParent<EnemyHealth>();
        group.alpha = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dialogueStarted)
            {
                lineIndex = 0;
                text.SetText(dialogueLines[lineIndex]);
                group.alpha = 1;
                dialogueStarted = true;

                if (dialogueStarted == true)
                {


                    //} else if (lineIndex < dialogueLines.Count) {
                    //    text.SetText(dialogueLines[lineIndex++]); //  
                    //}
                    //else {
                    //    group.alpha = 0;
                    //}
                }

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

        if(dialogueStarted == true)
            {
                timer = timer + Time.deltaTime;
                    if (timer >= timeTillChange)
                    {
                        if (lineIndex < dialogueLines.Count)
                        {
                            text.SetText(dialogueLines[lineIndex++]); //  
                        }
                    }

            if (isPlant)
            {
                anim.Play("Lehti_idle_New_1");
            }

        }

        if(isCloud == false)
        {
            if (trigger.triggered == true)
            {
                StartDialogue();
                }
            }


        if(enemyHealth == null)
        {
            return;
        } else

        {


            if (enemyHealth.health == 0)
            {
                lineIndex = 1;
                text.SetText(dialogueLines[lineIndex]);
                group.alpha = 1;
                dialogueStarted = true;
            }
        

        }
    }

    
}
