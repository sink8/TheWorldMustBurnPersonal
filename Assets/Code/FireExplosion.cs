using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class FireExplosion : MonoBehaviour
{

    public ParticleSystem particle;
    public Tilemap map;
    public Tilemap mapMoving;

    [SerializeField]
    private MapManager mapManager;

    public FireManager fireManager;

    public float burnRadius = 2f;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapMoving = GameObject.FindGameObjectWithTag("MovingMap").GetComponent<Tilemap>();
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        fireManager = GameObject.FindGameObjectWithTag("FireManager").GetComponent<FireManager>();
    }

    public void PaikallinenRäjähdys(){
        PlayParticle();
        BurnFromObjectPosition();
    }

    void PlayParticle(){
        particle.Play();
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
                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
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
                    if (fireManager.activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    fireManager.SetTileOnFireMoving(gpos, data);
                }
            } //else Debug.DrawLine(playerPosition, pos, Color.red);
        }
    }

}
