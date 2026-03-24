using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 5.0f;
    private bool hasTriggered = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.CompareTag("Player") && !hasTriggered)
            {

            hasTriggered = true; // Mark as triggered
            StartCoroutine(SwitchMusicWithDelay());

        }


    }
    IEnumerator SwitchMusicWithDelay()
    {
        // 1. Stop the current music immediately
        AudioFW.StopLoop("ForestDrama");

        // 2. Wait for the specified time
        yield return new WaitForSeconds(delaySeconds);

        // 3. Start the new music
        AudioFW.PlayLoop("WeirdBattle");
    }


}
