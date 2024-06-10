using System.Collections.Generic;
using UnityEngine;

public class TriggerLongFire : MonoBehaviour
{
    public List<Animator> animList;
    public GameObject airUp;
    public Animator smoke1, smoke2;

    void Start()
    {

    }



    void StartAnimations()
    {
        for (int i = 0; i < animList.Count; i++)
        {
            animList[i].Play("FireUpNewAnimation");
        }

        smoke1.Play("Smoke2_anim");
        smoke2.Play("Smoke2_anim");
        airUp.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartAnimations();
        }
    }
}
