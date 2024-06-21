using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    
    void Start()
    {
        SerializeData();
        DesirializeData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SerializeData()
    {
        //var json = JsonUtility.ToJson(new SerializedData());
        var json = JsonConvert.SerializeObject(new SerializedData());
        File.WriteAllText(Application.dataPath + "savedata.json", json);
    }

    public void DesirializeData()
    {
        var json = File.ReadAllText(Application.dataPath + "savedata.json");
        Debug.Log(json);
        var serializedData = JsonConvert.DeserializeObject<SerializedData>(json);
        Debug.Log(serializedData);
    }
}
