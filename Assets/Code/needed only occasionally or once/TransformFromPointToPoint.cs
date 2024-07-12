using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFromPointToPoint : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    [SerializeField] Transform returnPoint;
    void Start()
    {
        player = GameObject.Find("TestPlayerRay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(WaitTillSecret());
            
        }
    }

    IEnumerator WaitTillSecret()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position = new Vector3(returnPoint.transform.position.x, returnPoint.transform.position.y, returnPoint.transform.position.z);
    }
}
