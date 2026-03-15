using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AshTilesColliding : MonoBehaviour
{

    [Header("Speed Settings")]
    [Tooltip("How fast the visual fade happens (Higher = Faster)")]
    float dissolveSpeed = 0.6f;
    [Tooltip("Delay before the next tile breaks (Lower = Faster spread)")]
    float chainSpreadDelay = 0.9f;

    public SimplePlayerControllerDoubleJump playercontroller;
    public GameObject player;
    public FireManager fireManager;
    Material dissolveMat;
    bool isDissolving = false;
    public bool isDissolvingOther = false;

    float fade = 1f;
    float fade2 = 1.5f;

    public List<GameObject> go = new List<GameObject>();

    Collider2D m_Collider;
    public Transform m_NewTransform;
    Vector3 m_Point;

    public int frames; 
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playercontroller = player.GetComponent<SimplePlayerControllerDoubleJump>();
        fireManager = GameObject.FindObjectOfType<FireManager>();
        dissolveMat = GetComponent<SpriteRenderer>().material;
        

        m_Collider = GetComponent<Collider2D>();
        m_Point = new Vector3(-5.3f, -0.4f, 0);
    }


    //private void OnCollisionEnter2D(Collision2D collision) {


    //    if (collision.transform.tag == "Ash" ) {
    //        if (!go.Contains(collision.gameObject))
    //            go.Add(collision.gameObject);
    //        //isDissolvingOther = true;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision) {

    //        if (collision.gameObject.CompareTag("DashBox")) {
    //            GetComponent<Collider2D>().enabled = false;
    //            isDissolving = true;
    //                gameObject.layer = 8;
    //        }
    //}

    private void Update() {
        // If the flag is set (by player dash or by a neighbor), run the dissolve
        if (isDissolving || isDissolvingOther) {
            // Force isDissolving to true so DissolveFunctio logic works
            isDissolving = true;
            DissolveFunctio();
        }
    }
    //private void Update() {
    //    /*if (m_Collider.bounds.Contains(m_Point)) {
    //        Debug.Log("Bounds contain the point : " + m_Point);
    //    }*/

    //    frames = frames + 1;
    //    frames = +1;

    //    //Debug.Log(frames); 

    //    for (var i = go.Count - 1; i > -1; i--) {
    //        if (go[i] == null)
    //            go.RemoveAt(i);
    //    }

    //    if (isDissolving == true) {
    //        DissolveFunctio();
    //    }

    //    if(isDissolvingOther == true) {
    //        DissolveFunctio();
    //    }

    //    //if (isDissolving == true) {
    //       /* if (isDissolvingOther == true) {
    //            fade2 -= Time.deltaTime;

    //            if (fade2 <= 0f) {
    //                fade2 = 0f;
    //                isDissolving = false;
    //                Destroy(gameObject);
    //                fireManager.Ashes.Remove(gameObject);
    //            }
    //            dissolveMat.SetFloat("_Dissolve", fade2);
    //        }*/
    //    //}
    //}
    void DissolveFunctio() {
        // (isDissolving == true) {

            // CONTROL 2: Visual fade speed
            fade -= Time.deltaTime * dissolveSpeed;

            if (fade <= 0f) {
                fade = 0f;
                isDissolving = false;
                Destroy(this.gameObject);
            }
            dissolveMat.SetFloat("_Dissolve", fade);
        
    }

    public void BreakTile() {
        if (isDissolving) return;
        isDissolving = true;

        // Immediate physics disable
        if (m_Collider != null) m_Collider.enabled = false;
        gameObject.layer = 8;

        // Start the spread
        SpreadToNeighbors();
    }
    void SpreadToNeighbors() {
        float scanRadius = 1.1f;
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(transform.position, scanRadius);

        foreach (var col in neighbors) {
            if (col.gameObject != this.gameObject && col.CompareTag("Ash")) {
                var neighborScript = col.GetComponent<AshTilesColliding>();
                if (neighborScript != null && !neighborScript.isDissolving) {
                    // CONTROL 1: Spread speed
                    neighborScript.StartCoroutine(neighborScript.ChainReactionDelay(chainSpreadDelay));
                }
            }
        }
    }
    public IEnumerator ChainReactionDelay(float delay) {
        yield return new WaitForSeconds(delay);
        BreakTile();
    }
    void DissolveOtherFunctio() {
        // (isDissolving == true) {
        fade -= Time.deltaTime;

        if (fade <= 0f) {
            
            fade = 0f;
            isDissolving = false;
            Destroy(gameObject);
            //fireManager.Ashes.Remove(gameObject);
        }
        dissolveMat.SetFloat("_Dissolve", fade);
        //
    }
}
