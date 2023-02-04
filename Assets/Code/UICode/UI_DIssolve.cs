using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_DIssolve : MonoBehaviour
{
    Material dissolveMat;
    
    float fade = 1f;
    bool isDissolving = false;

    void Start()
    {
        dissolveMat = GetComponent<Image>().material;
    }

    private void Update() {
        if (isDissolving == true) {
            DissolveFunctio();
        }
    }

        public void DissolveFunctio() {
            print("klicked");
            fade -= Time.deltaTime * 40;

            if (fade <= -90f) {
                print("fade happened");
                isDissolving = false;
                fade = -90;
            }

            dissolveMat.SetFloat("_CutOff", fade);
    }

    public void KlickDissolve(){
            isDissolving = true;
    }

    public void Dissolvenull() {
        StartCoroutine(waiter());
    }


    IEnumerator waiter() {
        yield return new WaitForSeconds(3);
        fade = 20;
        dissolveMat.SetFloat("_CutOff", fade);
    }

}
