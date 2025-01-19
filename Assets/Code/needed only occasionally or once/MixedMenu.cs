using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SecretManager.Instance.LoadSecrets();
        SecretManager.Instance.GetTotalFoundSecrets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
