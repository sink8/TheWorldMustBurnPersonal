using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinesCode : MonoBehaviour
{
    [SerializeField] Material mat;
    float dissolveAmount;
    float dissolveAmount2;
    bool isDissolving;
    float dissolveSpeed = 0.3f;
    [SerializeField] ParticleSystem par1;
    [SerializeField] ParticleSystem par2;
    [SerializeField] ParticleSystem par3;

    void Start()
    {
        mat.SetFloat("_DissolveAmount", 0);
        mat.SetFloat("_paksuus", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime * dissolveSpeed);
            mat.SetFloat("_DissolveAmount", dissolveAmount);

            dissolveAmount2 = Mathf.MoveTowards(dissolveAmount2, -1f, Time.deltaTime * 0.5f);

            mat.SetFloat("_paksuus", dissolveAmount2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            isDissolving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sparks"))
        {
            isDissolving = true;
            par1.Play();
            par2.Play();
            par3.Play();

        }
    }


}
