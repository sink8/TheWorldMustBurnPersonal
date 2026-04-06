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

    public PlayerHealth playerHealth;

    void Start(){
        levels = GameObject.FindGameObjectsWithTag("Level");

    }

    public void UnstuckPlayer() {
        playerHealth.Damaged(1);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    SecretManager.Instance.SaveSecrets();
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    SecretManager.Instance.LoadSecrets();
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("added secrets d: ");
        //    //SecretManager.Instance.AddSecret(levelNum, "secret1");

        //    SecretManager.Instance.AddSecret(2, "secret1");
        //    SecretManager.Instance.AddSecret(1, "secret3");
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("Total secrets found: " + SecretManager.Instance.GetTotalFoundSecrets());

        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    SecretManager.Instance.ResetSecrets();
        //    Debug.Log("secrets cleared ");
        //}
    }

    public void ReloadCurrentLevel()
    {
        Time.timeScale = 1f;
        if (currentLevel != null)
        {
            Debug.Log("Destroying current level...");
            Destroy(currentLevel);
            StartCoroutine(InstantiateLevelAfterDelay());
        }
        else
        {
            Debug.LogWarning("No current level to destroy. Loading level directly...");
            InstantiateLevel();
        }
    }

    private IEnumerator InstantiateLevelAfterDelay()
    {
        yield return new WaitForEndOfFrame(); // Ensure the level is fully destroyed
        InstantiateLevel();
    }

    private void InstantiateLevel()
    {
        if (levelNum - 1 >= 0 && levelNum - 1 < levelsAvailable.Length)
        {
            Debug.Log("Loading current level: " + (levelNum - 1));
            currentLevel = Instantiate(levelsAvailable[levelNum - 1], Vector3.zero, Quaternion.identity);
            currentLevel.SetActive(true);
            UpdatePlayerReference();

            menuAudio.StopMenuMusic();
        }
        else
        {
            Debug.LogError("Invalid level number: " + levelNum);
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
        UpdatePlayerReference();

        menuAudio.StopMenuMusic();

    }

    public IEnumerator LoadLevelsDelay( int level, float delay) {
        yield return new WaitForSeconds(delay);

        //FindCurrentLevelNumber();
        currentLevel = Instantiate(levelsAvailable[level-1]) as GameObject;
        currentLevel.transform.position = new Vector3(0, 0, 0);
        //Instantiate(levelsAvailable[level], new Vector3(0, 0, 0), Quaternion.identity);
        UpdatePlayerReference();
            menuAudio.StopMenuMusic();
        Debug.Log("load levels delay end ");
        //yield return new WaitForSeconds(delay);
    }

    public void StartLoadLevelsCoroutine(){
        StartCoroutine(LoadLevelsDelay(levelNum, delay));
    }

    public void DestroyCurrentLevel(){
        Debug.Log("destroy cur level level");
        Destroy(currentLevel);
        StartCoroutine(LoadLevelsDelay(levelNum, delay));

    }
    
    public void LoadCurrentLevel(){
        Debug.Log("load current level");
        currentLevel = Instantiate(levelsAvailable[levelNum-1]) as GameObject;
        currentLevel.transform.position = new Vector3(0, 0, 0);
    }

    public void DestroyLevel() {
        Debug.Log("destroy level");
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

    private void UpdatePlayerReference() {
        if (currentLevel != null) {
            playerHealth = currentLevel.GetComponentInChildren<PlayerHealth>();

            if (playerHealth == null) {
                Debug.LogWarning("PlayerHealth component not found in the instantiated level!");
            } else {
                Debug.Log("PlayerHealth reference updated successfully.");
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
