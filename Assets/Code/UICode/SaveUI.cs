using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using UnityEngine.SceneManagement;
public class SaveUI : MonoBehaviour
{
public static SaveUI instance;
    public SavesInfo activeInfo;
    public GameObject SavesMenu, alreadyExists;

    public TMP_Text Save1;
    public TMP_Text Save1x;
    public TMP_Text Save2;
    public TMP_Text Save2x;
    public TMP_Text Save3;
    public TMP_Text Save3x;
    public Button saveButton1;
    public Button saveButton2;
    public Button saveButton3;

    public string savenameThis;

    private string save1;
    private string save2;
    private string save3;
    public int saveNumber;

    public GameObject player;

    public bool saveExists;
    [SerializeField] GameObject cont;
    [SerializeField] GameObject loads;
    void Start()
    {
        instance = this;

        saveExists = SaveExists(); 
        if(saveExists == false)
        {
            print("false");
            cont.SetActive(false);
            loads.SetActive(false);
            LoadBinInfo();
            UpdateSaveText();
        }
        LoadBinInfo();
        UpdateSaveText();
    }

    // Update is called once per frame
    void Update()
    {

        //if(Input.GetKeyDown(KeyCode.Y)){
        //SaveBinInfo();
        //}

        //if(Input.GetKeyDown(KeyCode.P)){
        //LoadBinInfo();
        //}

        //if(Input.GetKeyDown(KeyCode.H)){
        //DeleteInfoBin();
        //}
    }
   public void UpdateSaveText(){
       if(activeInfo.testbool1 == true){
           Save1.text = "Save1";
           Save1x.text = "Save1";
           saveButton1.interactable = true;
       }
        if(activeInfo.testbool2 == true){
           Save2.text = "Save2";
           Save2x.text = "Save2";
           saveButton2.interactable = true;
       }
        if(activeInfo.testbool3 == true){
           Save3.text = "Save3";
           Save3x.text = "Save3";
           saveButton3.interactable = true;
       }

   }
   public void LoadSave(string saveName){
       SaveManager.instance.activeSave.saveName = saveName;
        
        SaveManager.instance.LoadBin();
        player.transform.position = new Vector3(SaveManager.instance.activeSave.respawnPosition[0], SaveManager.instance.activeSave.respawnPosition[1], SaveManager.instance.activeSave.respawnPosition[2]);
        if (saveName == "save1"){
            activeInfo.lastSaveNumb = 1;
            saveNumber = 1;
            SaveManager.instance.SaveBin();
        }
        if(saveName == "save2"){
            activeInfo.lastSaveNumb = 2;
            saveNumber = 2;
            SaveManager.instance.SaveBin();
        }
        if(saveName == "save3"){
            activeInfo.lastSaveNumb = 3;
            saveNumber = 3;
            SaveManager.instance.SaveBin();
        }
   }
    public void ContinueGame(){
        savenameThis = SaveManager.instance.activeSave.saveName;
        SaveManager.instance.LoadBin();
        player.transform.position = new Vector3(SaveManager.instance.activeSave.respawnPosition[0], SaveManager.instance.activeSave.respawnPosition[1], SaveManager.instance.activeSave.respawnPosition[2]);
        Debug.Log("continue" + SaveManager.instance.activeSave.respawnPosition[0] + " mmm ");
        if (savenameThis == "save1"){
            activeInfo.lastSaveNumb = 1;
            saveNumber = 1;
            SaveManager.instance.SaveBin();
        }
        if(savenameThis == "save2"){
            activeInfo.lastSaveNumb = 2;
            saveNumber = 2;
            SaveManager.instance.SaveBin();
        }
        if(savenameThis == "save3"){
            activeInfo.lastSaveNumb = 3;
            saveNumber = 3;
            SaveManager.instance.SaveBin();
        }

        // if(activeInfo.lastSaveNumb == 1){
        //     SaveManager.instance.activeSave.saveName = "save1";
        //     saveNumber = 1;
        //     SaveManager.instance.SaveBin();
        // }
        //        if(activeInfo.lastSaveNumb == 2){
        //     SaveManager.instance.activeSave.saveName = "save2";
        //     saveNumber = 2;
        //     SaveManager.instance.SaveBin();
        // } 
        //         if(activeInfo.lastSaveNumb == 3){
        //     SaveManager.instance.activeSave.saveName = "save3";
        //     saveNumber = 3;
        //     SaveManager.instance.SaveBin();
        // }

    }

    public void SaveBinInfo(){
    if(!Directory.Exists("FireSaves"))
        Directory.CreateDirectory("FireSaves");

    BinaryFormatter formatter = new BinaryFormatter();
    string dataPath = Application.persistentDataPath + "/fire" + SceneManager.GetActiveScene().buildIndex;

    FileStream stream = File.Create("FireSaves/" + activeInfo.saveNameInfo + ".bin");
    formatter.Serialize(stream, activeInfo);
    stream.Close();

}

public void LoadBinInfo(){

    BinaryFormatter formatter = new BinaryFormatter();

    FileStream stream = File.Open("FireSaves/" + activeInfo.saveNameInfo + ".bin", FileMode.Open);
    SavesInfo loadData = (SavesInfo) formatter.Deserialize(stream);
    activeInfo = loadData;
    stream.Close();

}

public void DeleteInfoBin(){
    if(System.IO.File.Exists(Directory.GetCurrentDirectory().ToString() + "FireSaves/" + activeInfo.saveNameInfo + ".bin")){
        File.Delete(Directory.GetCurrentDirectory().ToString() + "FireSaves/" + activeInfo.saveNameInfo + ".bin");
    }
}


    public void CloseSavesMenu() {

        SavesMenu.SetActive(false);
    }

    public void OpenAlreadyExists() {

        alreadyExists.SetActive(true);
    }

    public void CloseAlreadyExists() {

        alreadyExists.SetActive(false);
    }

    public void CreateNewSave(int number){
        if(number == 1){

            if(activeInfo.testbool1 == false) {
            SaveManager.instance.activeSave.saveName = "save1";
            activeInfo.testbool1 = true;
            activeInfo.lastSaveNumb = 1;
                saveNumber = 1;
                SaveBinInfo();
                SaveManager.instance.SaveBin();
            } else
                {
                    saveNumber = 1;
                    OpenAlreadyExists();
                    CloseSavesMenu();
                    // jos on jo olemassa, kysy tuhontaanko vanha, tuhoa ja tee uusi save tämän tilalle
                }
        }

        if(number == 2){
            if(activeInfo.testbool2 == false) {
            SaveManager.instance.activeSave.saveName = "save2";
            activeInfo.testbool2 = true;
            activeInfo.lastSaveNumb = 2;
            saveNumber = 2;
            SaveBinInfo();
            SaveManager.instance.SaveBin();
                } else {
                    saveNumber = 2;
                    OpenAlreadyExists();
                    CloseSavesMenu();
                    // jos on jo olemassa, kysy tuhontaanko vanha, tuhoa ja tee uusi save tämän tilalle
                }
        }

        if(number == 3){
            if(activeInfo.testbool3 == false) {
            SaveManager.instance.activeSave.saveName = "save3";
            activeInfo.testbool3 = true;
            activeInfo.lastSaveNumb = 3;
            saveNumber = 3;
            SaveBinInfo();
            SaveManager.instance.SaveBin();
                }else {
                    saveNumber = 3;
                    OpenAlreadyExists();
                    CloseSavesMenu();
                    // jos on jo olemassa, kysy tuhontaanko vanha, tuhoa ja tee uusi save tämän tilalle
                }
            }
    }


    public bool SaveExists()
    {
        string path = Path.Combine("FireSaves", activeInfo.saveNameInfo + ".bin");

        // Check if the file exists
        if (File.Exists(path))
        {
            // Check if the file has any content
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length > 0)
            {
                Debug.Log("Save file exists and contains data.");
                return true;
            }
        }

        Debug.LogWarning("Save file does not exist or is empty.");
        return false;
    }

    public void SaveOver1(){
        NewGamePlayerPosition();

        if(saveNumber == 1) {
        SaveManager.instance.activeSave.saveName = "save1";
        activeInfo.lastSaveNumb = 1;
            SecretManager.Instance.ClearSecretsSave(1);
            SaveBinInfo();
            SaveManager.instance.SaveBin();
        }
        if(saveNumber == 2) {
        SaveManager.instance.activeSave.saveName = "save2";
        activeInfo.lastSaveNumb = 2;
        SecretManager.Instance.ClearSecretsSave(2);
            SaveBinInfo();
            SaveManager.instance.SaveBin();
        }
        if(saveNumber == 3) {
        SaveManager.instance.activeSave.saveName = "save3";
        activeInfo.lastSaveNumb = 3;
            SecretManager.Instance.ClearSecretsSave(3);
            SaveBinInfo();
            SaveManager.instance.SaveBin();
        }
    }

    void NewGamePlayerPosition() {
        SaveManager.instance.activeSave.respawnPosition[0] = player.transform.position.x;
        SaveManager.instance.activeSave.respawnPosition[1] = player.transform.position.y;
        SaveManager.instance.activeSave.respawnPosition[2] = player.transform.position.z;
    }


} // class


[System.Serializable]
public class SavesInfo {


    public string saveNameInfo;
    public int lastSaveNumb;
    public bool testbool1;
    public bool testbool2;
    public bool testbool3;

}