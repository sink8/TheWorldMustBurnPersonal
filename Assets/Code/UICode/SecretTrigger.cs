using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Cinemachine.DocumentationSortingAttribute;

public class SecretTrigger : MonoBehaviour
{
    // kun kyseinen secret löydetty ensimmäisen kerran, ei enää voi triggeröidä uudestaan
    // eli secrets täytyy tallentaa jonnekin

    //[SerializeField] SecretManager manager;
    int levelNumberTämä = 1;
    int level;
    LevelEnd LevelEnd;
    public string secretName;
    [SerializeField] bool secretLevelBool;
    [SerializeField] Transform secretLevelPoint;
    [SerializeField] AudioSource audioSource;
    

    GameObject player;

    Animator animator;

    void Start()
    {
        //manager = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<SecretManager>();
        LevelEnd = FindObjectOfType<LevelEnd>();
        levelNumberTämä = LevelEnd.LevelNumber;
        player = GameObject.Find("TestPlayerRay");

        // jos secret jo löytyy, niin se postetaan kentän aluksi
        print(secretName);
        if (SecretManager.Instance.HasFoundSecret(levelNumberTämä, secretName) == true)
        {
            print(" löytyi");
            gameObject.SetActive(false);
        }
        else
        {
            print("not found");
        }

    }


    void SaveSecretFound()
    {
        //SaveManager.instance.activeSave.respawnPosition[0] = player.transform.position.x;
        var secr = SaveManager.instance.activeSave.secretsFound[levelNumberTämä - 1];
        //bronceHighSeconds = SaveManager.instance.activeSave.bronceHighSecondsSave;
        var maxSec = SaveManager.instance.Maxsecrets[levelNumberTämä -1];
        if(secr <= 0)
        {
            print("ei löydettyjä");
        }

        if(maxSec == secr)
        {

        }

        for (int i = 0; i < SaveManager.instance.activeSave.secretsFound.Length; i++)
        {
            if (level == i + 1)
            {
                //levelSelector.LoadLevels(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sparks"))
        {
            if (SecretManager.Instance.HasFoundSecret(levelNumberTämä, secretName) == false)
            {

                //AudioFW.Play("Secret");
                //print("joooo");
                if (secretLevelBool) {
                    StartCoroutine(WaitTillSecret());
                }
                
            }
            // Add the secret to the found secrets
            AudioFW.Play("Secretpoimi");
            SecretManager.Instance.AddSecret(levelNumberTämä, secretName);

            
            // Optionally, you might want to hide or deactivate the secret after it's found
            gameObject.SetActive(false);

            

        }
    }


    IEnumerator WaitTillSecret()
    {
        print("coroutine started");
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(secretLevelPoint.transform.position.x, secretLevelPoint.transform.position.y, secretLevelPoint.transform.position.z); ;
    }


}
