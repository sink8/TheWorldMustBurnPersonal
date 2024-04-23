using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    //public TileBase[] burnedTiles; ideana ett� jokaisella tilell� olisi data burnedtiles. T�ss� esim gameobject johon prafab.
    public GameObject burned;
    
    public float spreadChange = 100; 
    public float spreadInterwall, burnTime;
    public float spreadInterwallGround, groundTime;
    public float burnedPrefabTime = 1.5f;
    public bool canBurn, ashTile, groudTile, waterTile, leavesTile, secret, snowTile, canSmoke, softGround;
    public ParticleSystem secretParticle;
    public GameObject smokeParticle;

}
