using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BurningMovingObject : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private MapManager mapManager;

    [SerializeField]
    private Fire firePrefab;

    public FireManager fireManager;
    public float burnRadius = 1.5f;
    
    // Start is called before the first frame update

    public GameObject fire;
    void Start()
    {

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        fireManager = GameObject.FindGameObjectWithTag("FireManager").GetComponent<FireManager>();
        //burnO = GetComponent<BurningMovingObject>();

        
    }

    // Update is called once per frame
    void Update()
    {
        BurnFromObjectPosition();
        TouchWaterY();
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
    }

    void TouchWaterY() {
        Vector2 playerPosition = this.transform.position;
        Vector3Int playergridPos = map.WorldToCell(playerPosition);
        playergridPos.y -= 1;

        TileData data = mapManager.GetTileData(playergridPos);
        if (map.HasTile(playergridPos) && data.waterTile == true) {
            fire.SetActive(false);
            //sr.enabled = false;
            //anim.enabled = false;
        }
    }
}
