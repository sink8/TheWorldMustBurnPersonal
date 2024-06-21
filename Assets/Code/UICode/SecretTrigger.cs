using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Cinemachine.DocumentationSortingAttribute;

public class SecretTrigger : MonoBehaviour
{
    // kun kyseinen secret löydetty ensimmäisen kerran, ei enää voi triggeröidä uudestaan
    // eli secrets täytyy tallentaa jonnekin

    //[SerializeField] SecretManager manager;
    public int levelNumber = 1;
    int level;
    LevelEnd LevelEnd;
    public string secretName;

    void Start()
    {
        //manager = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<SecretManager>();
        LevelEnd = FindObjectOfType<LevelEnd>();
    }

    
    void Update()
    {
        
    }

    void SaveSecretFound()
    {
        //SaveManager.instance.activeSave.respawnPosition[0] = player.transform.position.x;
        var secr = SaveManager.instance.activeSave.secretsFound[levelNumber - 1];
        //bronceHighSeconds = SaveManager.instance.activeSave.bronceHighSecondsSave;
        var maxSec = SaveManager.instance.Maxsecrets[levelNumber -1];
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
        if (other.CompareTag("Player"))
        {
            // Add the secret to the found secrets
            SecretManager.Instance.AddSecret(levelNumber, secretName);

            // Optionally, you might want to hide or deactivate the secret after it's found
            gameObject.SetActive(false);
        }
    }


}
