using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using static UnityEditor.PlayerSettings;

public class SparksMoving : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float lifeTime;
    Vector3 lastVelocity;
    public bool goingUp = true;
    public Vector3 launchDirection;

    public GameObject player;
    public Transform projectileEndParticle;
    [SerializeField] Transform spritepic;

    public Tilemap map;
    public Tilemap mapMoving;

    [SerializeField]
    private MapManager mapManager;

    public FireManager fireManager;

    [SerializeField]
    private GameObject explosionPre, explosionPreSmall, explosionPreSmallest;

    public Vector3 hitPosition;

    public float burnRadius = 1.5f;

    public BoundsInt bounds;
    public Vector2 pos;
    public GameObject dashboxPos;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        var scalepar = player.transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapMoving = GameObject.FindGameObjectWithTag("MovingMap").GetComponent<Tilemap>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        fireManager = GameObject.FindGameObjectWithTag("FireManager").GetComponent<FireManager>();
        //rb.velocity = launchDirection.normalized * speed;
        /*
        if(scalepar == -1) {
            launchDirection.x = -20f;
            rb.velocity = launchDirection.normalized * speed;
        }
        else rb.velocity = launchDirection.normalized * speed;
        */
        //spritepic.rotation = transform.rotation;
        

        Invoke("DestroySpark", lifeTime);

    }
    void Update()
    {
        BurnFromObjectPosition();
        transform.Translate(transform.up * speed * Time.deltaTime);

        
        BurnFromPlayerPositionDashO(dashboxPos.transform);
    }

    void DestroySpark() {
        
        var projectileEndParticleclone = Instantiate(projectileEndParticle, transform.position, transform.rotation);
        Destroy(projectileEndParticleclone.gameObject,1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
    print("osui johonkin");
        
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);
        AudioFW.Play("Explosion1");
        DestroySpark();
    }

    void BurnFromObjectPosition() {
        Vector2 playerPosition = transform.position;
        Vector3Int playergridPos = map.WorldToCell(playerPosition);

        int gr = Mathf.FloorToInt(burnRadius + 0.5f);
        var bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);
        var rsq = burnRadius * burnRadius;

        foreach (var gpos in bounds.allPositionsWithin) {
            var pos = (Vector2)map.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileData(gpos);
            if (rsq >= (playerPosition - pos).sqrMagnitude) {

                Debug.DrawLine(playerPosition, pos, Color.white);
                if (map.HasTile(gpos) && data.canBurn == true) {
                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytetä palavaa uudestaan
                    fireManager.SetTileOnFire(gpos, data);
                }
            } else Debug.DrawLine(playerPosition, pos, Color.red);
        }

        foreach (var gpos in bounds.allPositionsWithin) {
            var pos = (Vector2)mapMoving.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileDataMoving(gpos);
            if (rsq >= (playerPosition - pos).sqrMagnitude) {

                Debug.DrawLine(playerPosition, pos, Color.white);
                if (mapMoving.HasTile(gpos) && data.canBurn == true) {
                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytetä palavaa uudestaan
                    fireManager.SetTileOnFireMoving(gpos, data);
                }
            } //else Debug.DrawLine(playerPosition, pos, Color.red);
        }
    }

    public void BurnFromPlayerPositionDashO(Transform dashpos)
    {
        //playerPosition2 = player.transform.position;
        //Vector3Int playergridPos = map.WorldToCell(playerPosition2);
        Vector2 dashPositionO = dashpos.transform.position;
        Vector3Int playergridPos = map.WorldToCell(dashPositionO);

        int gr = Mathf.FloorToInt(burnRadius + 0.5f);
        //var bounds = new BoundsInt(playergridPos, new Vector3Int(gr * 2 + 1, gr * 2 + 1, 1));
        bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);

        var rsq = burnRadius * burnRadius;

        foreach (var gpos in bounds.allPositionsWithin)
        {
            pos = (Vector2)map.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileData(gpos);
            if (rsq >= (dashPositionO - pos).sqrMagnitude)
            {

                Debug.DrawLine(dashPositionO, pos, Color.white);
                if (map.HasTile(gpos) && data.softGround)
                {
                    print("tekeekö tää mitään");
                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    fireManager.StartDestroyingSoftGround(gpos, data);
                    //SetTileOnFire(gpos, data);
                }
            }
            else Debug.DrawLine(dashPositionO, pos, Color.red);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.gameObject.CompareTag("DashBox"))
    //    {
    //        Debug.Log("dashbox");
    //        BurnFromDashPosition();
    //    }

    //}



}
