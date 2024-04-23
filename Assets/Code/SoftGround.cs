using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftGround : MonoBehaviour
{
    private Vector3Int position;
    public TileData data;
    public FireManager fireManager;

    public float groundTimeCounter, spreadInterwallCounter, groundPrefabCounter;
    bool burnedPrefabBool = false;
    public void StartBurningGround(Vector3Int position, TileData data, FireManager fm)
    {
        this.position = position;
        this.data = data;
        fireManager = fm;

        groundTimeCounter = data.groundTime;
        spreadInterwallCounter = data.spreadInterwallGround;
        groundPrefabCounter = data.burnedPrefabTime;
    }

    private void Update()
    {
        groundTimeCounter -= Time.deltaTime;
        if (data.leavesTile)
        {
            print("data.soft ground");
            if (groundTimeCounter <= 0)
            {
                print("data.soft ground 334334");
                fireManager.FinishedBurning(position);
                Destroy(gameObject);
            }
        }
        else
        {
            if (burnedPrefabBool == false)
            {
                groundPrefabCounter -= Time.deltaTime;
                if (groundPrefabCounter <= 0)
                {
                    fireManager.InstantiateBurnedPrefab(position);
                    burnedPrefabBool = true;
                    Destroy(gameObject, 3f);
                }
            }
        }

        spreadInterwallCounter -= Time.deltaTime;
        if (spreadInterwallCounter <= 0)
        {
            spreadInterwallCounter = data.spreadInterwallGround;
            fireManager.TryToSpread(position, data.spreadChange);
            
        }

    }
}
