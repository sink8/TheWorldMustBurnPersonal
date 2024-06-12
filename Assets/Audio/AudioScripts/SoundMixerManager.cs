using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider MasterSlider;

    private const string MusicVolumeKey = "musicVolume";
    private const string SFXVolumeKey = "soundFXVolume";
    private const string MasterVolumeKey = "MasterVolume";
    void Start()
    {
        LoadAllVolumes();
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusic();
        }else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("soundFXVolume"))
        {
            LoadMusicFX();
        }else
        {
            SetSoundFXVolume();
        }
    }

    private void Update()
    {
        SetMusicVolume();
        SetMasterVolume();
        SetSoundFXVolume();
    }


    public void SetMasterVolume()
    {

        float volume = MasterSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);

    }

    public void SetSoundFXVolume()
    {

        float volume = SFXSlider.value;
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);

        //float volume = Mathf.Log10(level) * 20f;
        //audioMixer.SetFloat("musicVolume", volume);
        ////audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        //PlayerPrefs.SetFloat("musicVolume", volume);
        //PlayerPrefs.Save();
    }


    void LoadMusicvanha()
    {
        //musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //SetMusicVolume();

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

        SetMusicVolume();
    }

    void LoadMusicFX()
    {
        //musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //SetMusicVolume();

        SFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume");

        SetSoundFXVolume();
    }


    private void SaveSFX(float value)
    {
        PlayerPrefs.SetFloat("soundFXVolume", value);
    }

    void LoadSFX(float value)
    {
        SFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume");
        SetSoundFXVolume();
    }

    private void LoadAllVolumes()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            LoadMusic();
        }
        else
        {
            SetMusicVolume(); // Set to default
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            LoadSoundFX();
        }
        else
        {
            SetSoundFXVolume(); // Set to default
        }

        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            LoadMaster();
        }
        else
        {
            SetMasterVolume(); // Set to default
        }
    }
    private void LoadMusic()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
        musicSlider.value = savedVolume;
        SetMusicVolume();
    }

    private void LoadSoundFX()
    {
        float savedVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
        SFXSlider.value = savedVolume;
        SetSoundFXVolume();
    }

    private void LoadMaster()
    {
        float savedVolume = PlayerPrefs.GetFloat(MasterVolumeKey);
        MasterSlider.value = savedVolume;
        SetMasterVolume();
    }
}
