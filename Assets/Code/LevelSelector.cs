using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelSelector : MonoBehaviour
{
    public MenuAudio menuAudio;

    [SerializeField] GameObject[] levels;
    MovingOnLevelsMap levelsScript;

    public GameObject[] levelsAvailable;
    public GameObject[] secretsLevelsAvailable;
    public GameObject currentLevel;

    public int levelNum;
    float delay = 0.5f;

    void Start(){
        levels = GameObject.FindGameObjectsWithTag("Level");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SecretManager.Instance.SaveSecrets();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            SecretManager.Instance.LoadSecrets();
        }
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("added secrets d: " );
        //    //SecretManager.Instance.AddSecret(levelNum, "secret1");

        //    SecretManager.Instance.AddSecret(2, "secret1");
        //    SecretManager.Instance.AddSecret(1, "secret3");
        //}
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Total secrets found: " + SecretManager.Instance.GetTotalFoundSecrets());
            
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SecretManager.Instance.ResetSecrets();
            Debug.Log("secrets cleared ");
        }
    }


    public void LoadLevels( int level) {
        //FindCurrentLevelNumber();
        print("leveli on " + level);
        levelNum = level + 1;
        currentLevel = Instantiate(levelsAvailable[level]) as GameObject;
        currentLevel.SetActive(true);
        currentLevel.transform.position = new Vector3(0, 0, 0);
            //Instantiate(levelsAvailable[level], new Vector3(0, 0, 0), Quaternion.identity);
            menuAudio.StopMenuMusic();

    }

    public IEnumerator LoadLevelsDelay( int level, float delay) {
        yield return new WaitForSeconds(delay);

        FindCurrentLevelNumber();
        currentLevel = Instantiate(levelsAvailable[level-1]) as GameObject;
        currentLevel.transform.position = new Vector3(0, 0, 0);
            //Instantiate(levelsAvailable[level], new Vector3(0, 0, 0), Quaternion.identity);
            menuAudio.StopMenuMusic();
        //yield return new WaitForSeconds(delay);
    }

    public void StartLoadLevelsCoroutine(){
        StartCoroutine(LoadLevelsDelay(levelNum, delay));
    }

    public void DestroyCurrentLevel(){
        Destroy(currentLevel);
        StartCoroutine(LoadLevelsDelay(levelNum, delay));

    }
    
    public void LoadCurrentLevel(){
        
        currentLevel = Instantiate(levelsAvailable[levelNum-1]) as GameObject;
        currentLevel.transform.position = new Vector3(0, 0, 0);
    }

    public void DestroyLevel() {
        Destroy(currentLevel);
    }


    public void FindCurrentLevelNumber(){
        print("find current level number lukee");
        for(int i = 0; i < levels.Length; i++){
            print(i);
            if( levels[i].GetComponent<MovingOnLevelsMap>().currentLevel == true ){
                levelNum = levels[i].GetComponent<MovingOnLevelsMap>().levelNumber;
            }
        }
    }


    // public void LoadLevel1() {
    //     Instantiate(level1, new Vector3(0, 0, 0),Quaternion.identity);
    //     menuAudio.StopMenuMusic();
    // }

    // public void LoadLevel2() {
    //     Instantiate(level2, new Vector3(0, 0, 0), Quaternion.identity);
    // }
    // public void LoadLevel3() {
    //     Instantiate(level3, new Vector3(0, 0, 0), Quaternion.identity);
    // }
}
