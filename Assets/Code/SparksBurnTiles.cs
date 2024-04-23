using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SparksBurnTiles : MonoBehaviour
{
    public Tilemap map;
    public Tilemap mapMoving;

    [SerializeField]
    private MapManager mapManager;

    public FireManager fireManager;

    [SerializeField]
    private GameObject explosionPre, explosionPreSmall, explosionPreSmallest;

    public Vector3 hitPosition;
    public Transform player;

    public float burnRadius = 1.5f;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapMoving = GameObject.FindGameObjectWithTag("MovingMap").GetComponent<Tilemap>();

    }



    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Sparks")) {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts) {
                // t‰h‰n ehk‰ jotain huomiota mist‰ suunnasta isku tulee. V‰lill‰ tekee v‰‰r‰n muutoksen
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                //map.SetTile(map.WorldToCell(hitPosition), null);

                if(hitPosition.y < 0) {
                    hitPosition.y = hit.point.y - 1f;
                }
                if (hitPosition.x < 0) {
                    hitPosition.x = hit.point.x - 0f;
                }

                if(player.localScale.x == -1) {
                    //print(" scale -1");
                    if (hitPosition.x < 0) {
                        hitPosition.x = hit.point.x - 1f;
                    }
                }


                var hitPosInt = ToInt3(hitPosition);

                TileData data = mapManager.GetTileData(hitPosInt);
                TileData data2 = mapManager.GetTileDataMoving(hitPosInt);

                if (map.HasTile(hitPosInt) && data.canBurn == true) {
                    if (fireManager.activeFires.Contains(hitPosInt)) return; // ei sytytet‰ palavaa uudestaan
                    fireManager.SetTileOnFire(hitPosInt, data);
                }
                if (mapMoving.HasTile(hitPosInt) && data2.canBurn == true) {
                    if (fireManager.activeFires.Contains(hitPosInt)) return; // ei sytytet‰ palavaa uudestaan
                    fireManager.SetTileOnFireMoving(hitPosInt, data2);
                }

                //InstantiateExplosion(hitPosInt, data, explosionPre);
                InstantiateExplosion(hitPosInt, data, explosionPreSmall);
                InstantiateExplosion(hitPosInt, data, explosionPreSmallest);
                InstantiateExplosion(hitPosInt, data2, explosionPreSmall);
                InstantiateExplosion(hitPosInt, data2, explosionPreSmallest);
            }

        }
        if (collision.gameObject.CompareTag("DashBox"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }
        if (collision.gameObject.CompareTag("DashDown"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DashBox"))
        {
            fireManager.dashTrigger = true;
            Debug.Log("dashbox");
            fireManager.BurnFromPlayerPositionDash();
        }
        if (collision.gameObject.CompareTag("DashDown"))
        {
            fireManager.dashTrigger = true;
            Debug.Log("dashboxdown");
            fireManager.BurnFromPlayerPositionDash();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DashBox"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }
        if (collision.gameObject.CompareTag("DashDown"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }
    }

    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DashBox"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }
        if (collision.gameObject.CompareTag("DashDown"))
        {
            fireManager.dashTrigger = false;
            //fireManager.BurnFromPlayerPositionDash();
        }
    }
    public void InstantiateExplosion(Vector3Int tilePosition, TileData data, GameObject prefab) {

        var explosion = Instantiate(prefab);
        explosion.transform.position = map.GetCellCenterWorld(tilePosition);
        //explosion.StartBurning(tilePosition, data, this);

    }

    public Vector3Int ToInt3(Vector3 v) {
        return new Vector3Int((int)v.x, (int)v.y, (int)v.z );
    }




}
