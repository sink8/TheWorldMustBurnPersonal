using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioFW.PlayLoop("MenuMusic");
    }

    private void Update() {
        
    }

    public void StopMenuMusic() {
        AudioFW.StopLoop("MenuMusic");
    }

    public void StartMenuMusic() {
        AudioFW.PlayLoop("MenuMusic");
    }

    public void StopBurningSound() {
        AudioFW.StopLoop("FireBurningLoop");
    }

    public void StartburningSound() {
        AudioFW.PlayLoop("FireBurningLoop");
    }
    private void OnEnable()
    {
        AudioFW.PlayLoop("MenuMusic");
    }

    private void OnDisable()
    {
        AudioFW.StopLoop("MenuMusic");
    }
}
