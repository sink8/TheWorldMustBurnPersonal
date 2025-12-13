using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;


    public class SecretManager : MonoBehaviour
{

    public static SecretManager Instance;
    public int totalAmountSecrets;
    public int secretdoor1 = 7;
    public int secretdoor2 = 17;
    public int secretdoor3 = 25;

    public int secretsFound = 0;

    public Dictionary<int, HashSet<string>> foundSecrets;

    public TMPro.TextMeshProUGUI Secret1, Secret2, Secret3, allFlowers;

    public int activeSaveForThis;
    [SerializeField] SaveUI saveUI;

    private int lastLoadedSave = -1;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            foundSecrets = new Dictionary<int, HashSet<string>>();
        } else {
            Destroy(gameObject);
            return;
        }

        activeSaveForThis =  saveUI.saveNumber ;
        LoadSecrets();
        GetTotalFoundSecrets();
        
        //activeSaveForThis = saveUI.saveNumber;
        // this made save manager to un enable
    }

    private void Update()
    {
        activeSaveForThis = saveUI.saveNumber;

        // ✅ Detect if the player switched saves
        if (activeSaveForThis != lastLoadedSave) {
            LoadSecrets();                // reload secrets from correct file
            GetTotalFoundSecrets();       // update count
            lastLoadedSave = activeSaveForThis;
            Debug.Log($"Secrets reloaded for save slot {activeSaveForThis}");
        }

        // ✅ Update UI
        Secret1.text = $"{secretsFound}/7";
        Secret2.text = $"{secretsFound}/17";
        Secret3.text = $"{secretsFound}/27";
        allFlowers.text = secretsFound.ToString();


    }
    // Method to add a found secret
    // Method to add a found secret
    public void AddSecret(int level, string secret)
    {
        if (!foundSecrets.ContainsKey(level))
        {
            foundSecrets[level] = new HashSet<string>();
        }

        foundSecrets[level].Add(secret);
    }

    // Method to check if a secret has been found
    public bool HasFoundSecret(int level, string secret)
    {
        if (foundSecrets.ContainsKey(level) && foundSecrets[level].Contains(secret))
        {
            return true;
        }

        return false;
    }

    public void ClearSecret(int level)
    {
        if (!foundSecrets.ContainsKey(level))
        {
            foundSecrets[level] = new HashSet<string>();
        }

        //foundSecrets[level].Add(secret);
        foundSecrets[level].Clear();
    }

    // Method to get all found secrets for a level
    public HashSet<string> GetFoundSecrets(int level)
    {
        if (foundSecrets.ContainsKey(level))
        {
            return foundSecrets[level];
        }

        return new HashSet<string>();
    }

    // Optionally, method to reset all secrets (e.g., for a new game)
    public void ResetSecrets()
    {
        foundSecrets.Clear();
    }

    // Method to save secrets
    public void SaveSecrets()
        {
        //Example implementation for saving secrets using JSON
        string path = Path.Combine(Application.persistentDataPath, $"save{activeSaveForThis}secrets.json");

        string json = JsonConvert.SerializeObject(new SerializableDictionary<int, HashSet<string>>(foundSecrets));
        // Write the file
        File.WriteAllText(path, json);
        Debug.Log("save stuff " + json + Application.dataPath);

        //var json = JsonConvert.SerializeObject(new SerializedData());
        //File.WriteAllText(Application.dataPath + "savedata.json", json);
    }

    public void ResetSecretsForSave(int saveSlot) {
        string path = Path.Combine(Application.persistentDataPath, $"save{saveSlot}secrets.json");
        if (File.Exists(path)) File.Delete(path);
    }

    // Method to load secrets
    public void LoadSecrets()
        {

        string path = Path.Combine(Application.persistentDataPath, $"save{activeSaveForThis}secrets.json");

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SerializableDictionary<int, HashSet<string>> data =
                JsonConvert.DeserializeObject<SerializableDictionary<int, HashSet<string>>>(json);
            foundSecrets = data.ToDictionary();
            Debug.Log($"Secrets loaded for save {activeSaveForThis}");
        } else {
            foundSecrets = new Dictionary<int, HashSet<string>>(); // ✅ fresh empty dict
            Debug.Log($"No secrets file found for save {activeSaveForThis}, starting clean.");
        }
        // Example implementation for loading secrets using JSON
        //string path = Application.dataPath + "/save" + activeSaveForThis.ToString() + "secrets.json";
        //    if (File.Exists(path))
        //    {
        //    string json = File.ReadAllText(path);
        //    SerializableDictionary<int, HashSet<string>> data = JsonConvert.DeserializeObject<SerializableDictionary<int, HashSet<string>>>(json);
        //    var ser = JsonConvert.DeserializeObject<SerializableDictionary<int, HashSet<string>>>(json);
        //    foundSecrets = data.ToDictionary();

        //    print("secrets loaded");
        //    Debug.Log(json);
        //    Debug.Log(ser);
        //}
        }


    public void ClearSecrets()
    {
        string path = Path.Combine(Application.persistentDataPath, $"save{activeSaveForThis}secrets.json");
        //string path = Application.dataPath + "/save" + activeSaveForThis.ToString() + "secrets.json";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Secrets file deleted: " + path);
        }
        else
        {
            Debug.Log("Secrets file not found: " + path);
        }
    }

    public void ClearSecretsSave(int i)
    {
        //string path = Application.dataPath + "/save" + i.ToString() + "secrets.json";
        string path = Path.Combine(Application.persistentDataPath, $"save{i}secrets.json");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Secrets file deleted: " + path);
        }
        else
        {
            Debug.Log("Secrets file not found: " + path);
        }
    }

    // Method to count total number of found secrets
    public int GetTotalFoundSecrets()
        {
            int total = 0;
            foreach (var secrets in foundSecrets.Values)
            {
                total += secrets.Count;
            }
        secretsFound = total;
            return total;
        }

    public int GetTotalFoundSecretsLevel(int level)
    {
        if (foundSecrets.ContainsKey(level))
        {
            return foundSecrets[level].Count;
        }
        return 0;
    }

    public void RefreshSecretsForActiveSave() {
        activeSaveForThis = saveUI.saveNumber;
        LoadSecrets();
        GetTotalFoundSecrets();
    }


}


    

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    public List<TKey> keys = new List<TKey>();
    public List<TValue> values = new List<TValue>();

    public Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
    {
        this.dictionary = dictionary;
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
    }



    public Dictionary<TKey, TValue> ToDictionary()
    {
        return dictionary;
    }
}




