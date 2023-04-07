using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMoving : MonoBehaviour
{
    private Vector3Int position;
    TileData data;
    FireManager fireManager;

    private float burnTimeCounter, spreadInterwallCounter, burnedPrefabCpunter;
    bool burnedPrefabBool = false;
    public void StartBurning(Vector3Int position, TileData data, FireManager fm) {
        this.position = position;
        this.data = data;
        fireManager = fm;

        burnTimeCounter = data.burnTime;
        spreadInterwallCounter = data.spreadInterwall;
        burnedPrefabCpunter = data.burnedPrefabTime;
    }

    private void Update() {
        burnTimeCounter -= Time.deltaTime;
        if (data.leavesTile) { 
        if (burnTimeCounter <= 0) {
            fireManager.FinishedBurningMoving(position);
            Destroy(gameObject);
        }
    } else {
            if (burnedPrefabBool == false) {
                burnedPrefabCpunter -= Time.deltaTime;
                if (burnedPrefabCpunter <= 0) {
                    fireManager.InstantiateBurnedPrefabMoving(position);
                    burnedPrefabBool = true;
                    Destroy(gameObject,3f) ;
                }
            }
        }

        spreadInterwallCounter -= Time.deltaTime;
        if (spreadInterwallCounter <= 0) {
            spreadInterwallCounter = data.spreadInterwall;
            fireManager.TryToSpreadMoving(position, data.spreadChange);
            fireManager.TryToSpread(position, data.spreadChange);
        }

    }

    void FinishedBurning() {
        burnTimeCounter -= Time.deltaTime;
        if (burnTimeCounter <= 0) {
            fireManager.FinishedBurningMoving(position);
            Destroy(gameObject);
        }

        spreadInterwallCounter -= Time.deltaTime;
        if (spreadInterwallCounter <= 0) {
            spreadInterwallCounter = data.spreadInterwall;
            fireManager.TryToSpreadMoving(position, data.spreadChange);
            fireManager.TryToSpread(position, data.spreadChange);
        }
        if (burnedPrefabBool == false) {
            burnedPrefabCpunter -= Time.deltaTime;
            if (burnedPrefabCpunter <= 0) {
                fireManager.InstantiateBurnedPrefabMoving(position);
                burnedPrefabBool = true;
            }
        }
    }
}
