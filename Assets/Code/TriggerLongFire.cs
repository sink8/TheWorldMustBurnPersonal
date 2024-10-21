using System.Collections.Generic;
using UnityEngine;

public class TriggerLongFire : MonoBehaviour
{
    public List<Animator> animList;
    public GameObject airUp;
    public Animator smoke1, smoke2;
    public string smokeName = "Smoke2_anim"; 

    void Start()
    {

    }



    void StartAnimations(string animation_)
    {
        for (int i = 0; i < animList.Count; i++)
        {
            animList[i].Play("FireUpNewAnimation");
        }

        smoke1.Play(animation_);
        smoke2.Play(animation_);
        airUp.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sparks"))
        {
            StartAnimations(smokeName);
        }
    }
}
