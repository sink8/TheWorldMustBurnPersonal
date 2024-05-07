using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonesRunes : MonoBehaviour
{
    
    public Material stoneMaterial;
    public bool isVisible = false;
    public float maxVisibility = 10f;
    public float maxAlpha = 0.8f;
    float minAlpha = 0f;
    public float timeToGrow = 5f; 
    public float timeToGrowLight = 5f;
    public float refreshRate = 0.05f;

    public float refreshRateLight = 0.05f;
    public float shininess;

    public bool hasAlfa = false;

    void Start()
    {
        stoneMaterial = GetComponentInChildren<SpriteRenderer>().material;
    }

    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G)) {
        //    print("jotain");
        //    EnableRuneLight();
        //}
        //ChangeRuneslightAlpha(stoneMaterial);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnableRuneLight();
        }
    }


    public void EnableRuneLight(){

        //if (!isVisible) {
        float shininess = Mathf.PingPong(Time.time, 9.0f);
        //stoneMaterial.SetFloat("_CutOff", shininess);
        StartCoroutine(GrowRunes(stoneMaterial));
        if (hasAlfa)
        {
            StartCoroutine(ChangeRunesAlpha(stoneMaterial));
        }
        
        //ChangeRuneslightAlpha(stoneMaterial);

        //}

    }

    IEnumerator GrowRunes (Material mat) {
        float growValue = mat.GetFloat("_CutOff");

        //if (!isVisible) {
            while( growValue < maxVisibility) {
                growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("_CutOff", growValue);

                yield return new WaitForSeconds(refreshRate);
            }
        //}
    }

    IEnumerator ChangeRunesAlpha(Material mat) {
        float growValue = mat.GetFloat("_alphaStrenght");

        if (!isVisible) {
            while (growValue < maxAlpha) {
                growValue += 1 / (timeToGrowLight / refreshRateLight);
                mat.SetFloat("_alphaStrenght", growValue);

                yield return new WaitForSeconds(refreshRateLight);
            }
        }
        else {
            while (growValue > minAlpha) {
                growValue -= 1 / (timeToGrowLight / refreshRateLight);
                mat.SetFloat("_alphaStrenght", growValue);

                yield return new WaitForSeconds(refreshRateLight);
            }

        }
        if(growValue >= maxAlpha) 
            isVisible = true;
            else
                isVisible = false;
        
    }

    void ChangeRuneslightAlpha(Material mat) {
        float growValue = mat.GetFloat("_alphaStrenght");

        if (!isVisible) {
            while (growValue < maxAlpha) {
                growValue += refreshRateLight * Time.deltaTime;
                mat.SetFloat("_alphaStrenght", growValue);

               
            }
        } else {
            while (growValue > minAlpha) {
                growValue -= refreshRateLight * Time.deltaTime;
                mat.SetFloat("_alphaStrenght", growValue);

                
            }

        }
        if (growValue >= maxAlpha)
            isVisible = true;
        else
            isVisible = false;

    }

}
