using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion_pref : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private MapManager mapManager;

    [SerializeField]
    private Fire firePrefab;

    public FireManager fireManager;
    public float burnRadius1 = 0.5f;
    float burnRadiusSave;
    public float burnRadiusLarge = 4f;
    public float radiusTime = 0;
    
    public bool radUpdating = false;

    void Start()
    {

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        fireManager = GameObject.FindGameObjectWithTag("FireManager").GetComponent<FireManager>();
        //burnO = GetComponent<BurningMovingObject>();

        burnRadiusSave = burnRadius1;
        radiusTime = burnRadius1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.O)) {
            radUpdating = true;
            
        }
            
        if(radUpdating)
        {
            UpdateBurnRadius(burnRadius1);
        }

        BurnFromObjectPosition();
        
    }

    void BurnFromObjectPosition()
    {
        Vector2 playerPosition = transform.position;
        Vector3Int playergridPos = map.WorldToCell(playerPosition);

        int gr = Mathf.FloorToInt(burnRadius1 + 0.5f);
        var bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);
        var rsq = burnRadius1 * burnRadius1;

        foreach (var gpos in bounds.allPositionsWithin)
        {
            var pos = (Vector2)map.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileData(gpos);
            if (rsq >= (playerPosition - pos).sqrMagnitude)
            {

                Debug.DrawLine(playerPosition, pos, Color.gray);
                if (map.HasTile(gpos) && data.canBurn == true)
                {

                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytetä palavaa uudestaan
                    fireManager.SetTileOnFire(gpos, data);
                }
            }
            else Debug.DrawLine(playerPosition, pos, Color.green);
        }
    }

    public void UpdateBurnRadius(float burnrad)
    {

        
            //if (maxRad == false)
                if (radiusTime < burnRadiusLarge)
                {
                print("updating");
                    burnrad += Time.deltaTime;
                    radiusTime += Time.deltaTime;
                    float t = radiusTime / burnrad;
                    burnRadius1 = radiusTime;
                } else
                {
                    
                    burnRadius1 = burnRadiusSave;
                    radUpdating = false;
                    radiusTime = burnRadiusSave;
                }
        
        
    }

}
