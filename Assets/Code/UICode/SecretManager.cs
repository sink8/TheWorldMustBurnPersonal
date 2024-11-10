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
    public int secretdoor1 = 5;
    public int secretdoor2 = 10;

    public int secretsFound = 0;

    public Dictionary<int, HashSet<string>> foundSecrets;

    public TMPro.TextMeshProUGUI Secret1, Secret2;

    public int activeSaveForThis;
    [SerializeField] SaveUI saveUI;

    void Awake()
    {
        // Implement Singleton pattern to ensure only one instance of SecretManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            foundSecrets = new Dictionary<int, HashSet<string>>(); // Initialize the dictionary
        }
        else
        {
            Destroy(gameObject);
        }

        LoadSecrets();
        
        activeSaveForThis = saveUI.saveNumber;
    }

    private void Update()
    {
        //secretsFound = GetTotalFoundSecrets();
        Secret1.text = secretsFound.ToString() + "/3";
        

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
            string json = JsonConvert.SerializeObject(new SerializableDictionary<int, HashSet<string>>(foundSecrets));
        File.WriteAllText(Application.dataPath + "/save" + activeSaveForThis.ToString() + "secrets.json", json);
        Debug.Log("save stuff " + json + Application.dataPath);

        //var json = JsonConvert.SerializeObject(new SerializedData());
        //File.WriteAllText(Application.dataPath + "savedata.json", json);
    }

        // Method to load secrets
        public void LoadSecrets()
        {
            // Example implementation for loading secrets using JSON
            string path = Application.dataPath + "/save" + activeSaveForThis.ToString() + "secrets.json";
            if (File.Exists(path))
            {
            string json = File.ReadAllText(path);
            SerializableDictionary<int, HashSet<string>> data = JsonConvert.DeserializeObject<SerializableDictionary<int, HashSet<string>>>(json);
            var ser = JsonConvert.DeserializeObject<SerializableDictionary<int, HashSet<string>>>(json);
            foundSecrets = data.ToDictionary();

            print("secrets loaded");
            Debug.Log(json);
            Debug.Log(ser);
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




