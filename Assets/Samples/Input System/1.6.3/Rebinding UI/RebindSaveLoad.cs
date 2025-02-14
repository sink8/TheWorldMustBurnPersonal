using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset actions;

    public void OnEnable()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actions.LoadBindingOverridesFromJson(rebinds);
            Debug.Log($"Loaded rebinds cgfhgfh: {rebinds}"); // Print the rebinds to the console

        }
        else
        {
            Debug.Log("No rebinds found in PlayerPrefs.");
        }
    }

public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        print("it reads something closing");
    }
}
