using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {

            LoadAudio();
        }
        else
        {
            LoadAudio();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    void LoadAudio()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

}


