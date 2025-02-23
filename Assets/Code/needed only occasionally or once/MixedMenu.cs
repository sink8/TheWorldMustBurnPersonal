using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedMenu : MonoBehaviour
{
    public GameObject tryAgainB;
    void Start()
    {
        SecretManager.Instance.LoadSecrets();
        SecretManager.Instance.GetTotalFoundSecrets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        tryAgainB.SetActive(false);
    }

    private void OnDisable()
    {
        tryAgainB.SetActive(true);
    }
}
