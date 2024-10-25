using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using TMPro;
using static ScoreBallsMovingToBar;
using UnityEngine.U2D;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    public Tilemap mapMoving;

    [SerializeField]
    private MapManager mapManager;

    [SerializeField]
    private Fire firePrefab;


    [SerializeField]
    private FireMoving firePrefab2;

    [SerializeField]
    private SoftGround softGround;

    Fire newFire;
    FireMoving newFire3;
    SoftGround softGroundNew;

    public GameObject allFires;

    RayCastPlayer raycastPlayer;
    public Transform player;
    public Transform dashPos;
    public Transform playerdashPosDown;
    public Transform burnedParticle;
    public Transform burnedParticleStart;
    public Transform meltedParticle;
    public Transform fireupParticle;

    public List<Vector3Int> activeFires = new List<Vector3Int>();

    public BoundsInt area;

    public float burnRadius = 1.5f;
    public float destRadius = 0.5f;

    public ScoreCounter scoreCounter;
    public Transform scoreInsPrefab;
    public float minSpawnDelay = 0.01f;
    public float maxSpawnDelay = 0.2f;

    public BoundsInt bounds;
    public Vector2 pos;
    public Vector2 playerPosition2;
    public Vector2 dashPosition2;

    public bool InstantiateLights = false;
    public bool dashDownTrigger = false;
    public bool dashTrigger = false;
    [SerializeField] GameObject fireLight;
    public List<GameObject> secrets = new List<GameObject>();

    public GameObject spritePrefab; // The sprite prefab with the HoveringMovement script attached
    public Vector2 spawnAreaMin; // Minimum bounds of the spawn area
    public Vector2 spawnAreaMax; // Maximum bounds of the spawn area
    //public GameObject targetPosition; // The fixed target position
    public RectTransform uiTarget; // The UI element to move towards
    public Canvas canvas; // The Canvas containing the UI element
    

    void Start() {
        //player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapMoving = GameObject.FindGameObjectWithTag("MovingMap").GetComponent<Tilemap>();

        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        scoreCounter = FindObjectOfType<ScoreCounter>();

        AudioFW.PlayLoop("FireBurningLoop");
        SecretBurnableEffects();
        raycastPlayer = player.GetComponent<RayCastPlayer>();
        //secrets = GameObject.FindGameObjectsWithTag("Secret");
        foreach(GameObject secr in GameObject.FindGameObjectsWithTag("Secret")) {
            secrets.Add(secr);
        }
        //targetPosition = GameObject.Find("BallTargetPos");

        GameObject tempObject = GameObject.Find("ScoreUIprefab");
        GameObject tempObject2 = GameObject.Find("StartPoint");

        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            canvas = tempObject.GetComponent<Canvas>();
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }
        if (tempObject2 != null)
        {
            //If we found the object , get the Canvas component from it.
            uiTarget = tempObject2.GetComponent<RectTransform>();
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject2.name);
            }
        }
    }


    public void TryToSpread(Vector3Int position, float spreadChange) {
        for (int i = position.x -1; i < position.x + 2; i++) {
            for (int j = position.y - 1; j < position.y + 2; j++) {
                TryToBurnTile(new Vector3Int(i,j,0));
            }
        }

        void TryToBurnTile(Vector3Int tilePostion) {
            if (activeFires.Contains(tilePostion)) return;
            TileData data = mapManager.GetTileData(tilePostion);

            if(data != null && data.canBurn) {

                if (Random.Range(50f, 100f) <= data.spreadChange)
                    SetTileOnFire(tilePostion, data);
            }
            if (data != null && data.softGround)
            {

                if (Random.Range(50f, 100f) <= data.spreadChange)
                    StartDestroyingSoftGround(tilePostion, data);
            }
        }
    }

    public void TryToSpreadMoving(Vector3Int position, float spreadChange) {
        for (int i = position.x - 1; i < position.x + 2; i++) {
            for (int j = position.y - 1; j < position.y + 2; j++) {
                TryToBurnTile(new Vector3Int(i, j, 0));
            }
        }

        void TryToBurnTile(Vector3Int tilePostion) {
            if (activeFires.Contains(tilePostion)) return;
            TileData data = mapManager.GetTileDataMoving(tilePostion);

            if (data != null && data.canBurn) {
                if (Random.Range(50f, 100f) <= data.spreadChange)
                    SetTileOnFireMoving(tilePostion, data);
            }
        }
    }

    public void SetTileOnFire(Vector3Int tilePosition, TileData data) {

        //map.SetColliderType(tilePosition, Tile.ColliderType.None);
        BurnedParticleKpinäts(tilePosition);

        Vector3Int tempTilepos = tilePosition;
        tempTilepos.y -= 1;
        //TileData dataunder = mapManager.GetTileData(tempTilepos);
        //Fire newFire = Instantiate(firePrefab);
        //Fire newFire2 = Instantiate(firePrefabup);
        newFire = Instantiate(firePrefab);
        newFire.transform.SetParent(allFires.transform);
        newFire.transform.position = map.GetCellCenterWorld(tilePosition);
        newFire.StartBurning(tilePosition, data, this);
        activeFires.Add(tilePosition);

        if(data.snowTile == true){
            MeltedParticles(tilePosition);
        }

        if(InstantiateLights == true) {
            InstantiateFireLights(tilePosition);
        }

        var randomNum = Random.Range(0, 4);
        if (randomNum == 1)
        {
            FireupParticles(tilePosition);
        }
        /*if (map.HasTile(tempTilepos) && dataunder.groudTile == true) {
            newFire = Instantiate(firePrefab);
            newFire.transform.SetParent(allFires.transform);
            newFire.transform.position = map.GetCellCenterWorld(tilePosition);
            newFire.StartBurning(tilePosition, data, this);
            activeFires.Add(tilePosition);
        } else  {
            newFire2 = Instantiate(firePrefabup);
            newFire2.transform.SetParent(allFires.transform);
            newFire2.transform.position = map.GetCellCenterWorld(tilePosition);
            newFire2.StartBurning(tilePosition, data, this);
            activeFires.Add(tilePosition);
        } */ // this pne had different fire animation for firest touching the ground
        //activeFires.Add(tilePosition);
    }

    public void SetTileOnFireMoving(Vector3Int tilePosition, TileData data) {

        BurnedParticleKpinäts(tilePosition);
        Vector3Int tempTilepos = tilePosition;
        tempTilepos.y -= 1;
        //TileData dataunder = mapManager.GetTileDataMoving(tempTilepos);

        newFire3 = Instantiate(firePrefab2);
        newFire3.transform.SetParent(allFires.transform);
        newFire3.transform.position = mapMoving.GetCellCenterWorld(tilePosition);
        newFire3.StartBurning(tilePosition, data, this);
        activeFires.Add(tilePosition);

        if(data.snowTile == true){
            MeltedParticles(tilePosition);
        }

        if (InstantiateLights == true) {
            InstantiateFireLights(tilePosition);
        }

        var randomNum = Random.Range(0, 4);
        if (randomNum == 1)
        {
            FireupParticles(tilePosition);
        }
        /*if (mapMoving.HasTile(tempTilepos) && dataunder.groudTile == true) {
            newFire3 = Instantiate(firePrefab2);
            newFire3.transform.SetParent(allFires.transform);
            newFire3.transform.position = mapMoving.GetCellCenterWorld(tilePosition);
            newFire3.StartBurning(tilePosition, data, this);
            activeFires.Add(tilePosition);
        } else {
            newFire4 = Instantiate(firePrefabup2);
            newFire4.transform.SetParent(allFires.transform);
            newFire4.transform.position = mapMoving.GetCellCenterWorld(tilePosition);
            newFire4.StartBurning(tilePosition, data, this);
            activeFires.Add(tilePosition);
        }*/
        //activeFires.Add(tilePosition);
    }


    private void Update() {
        PlayFireSound();

        BurnFromPlayerPosition();
        BurnFromPlayerPositionMoving();
        //BurnFromDashPosition();
        if(dashTrigger == true)
        {
            BurnFromPlayerPositionDashO(dashPos);
        }

        if (dashDownTrigger == true)
        {
            BurnFromPlayerPositionDashO(playerdashPosDown);
        }
        //var hitP = map.GetComponent<SparksBurnTiles>().hitPosition;

        //targetWorldPosition = UIUtilities.GetWorldPositionFromUI(canvas, uiTarget);
    }

    void BurnFromPlayerPosition() {
         playerPosition2 = player.transform.position;
        Vector3Int playergridPos = map.WorldToCell(playerPosition2);

        int gr = Mathf.FloorToInt(burnRadius + 0.5f);
        //var bounds = new BoundsInt(playergridPos, new Vector3Int(gr * 2 + 1, gr * 2 + 1, 1));
         bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);
        
        var rsq = burnRadius * burnRadius;

        /*for (int x = bounds.position.x ; x < (bounds.position.x + bounds.size.x); x++) {
            for (int y = bounds.position.y ; y < (bounds.position.y + bounds.size.y); y++) {
                TileData data = mapManager.GetTileData(new Vector3Int(x, y, 0));
                var pos = (Vector2)map.CellToWorld(new Vector3Int(x, y, 0)) + Vector2.one * 0.5f;

                if (rsq >= (playerPosition - pos).sqrMagnitude) {
                    if (map.HasTile(new Vector3Int(x, y, 0)) && data.canBurn == true) {
                        if (activeFires.Contains(new Vector3Int(x, y, 0))) return;
                        SetTileOnFire(new Vector3Int(x, y, 0), data);
                    }
                }   
            }
        }*/

        foreach (var gpos in bounds.allPositionsWithin) {
             pos = (Vector2) map.CellToWorld(gpos) + Vector2.one*0.5f;
            TileData data = mapManager.GetTileData(gpos);
                if (rsq >= (playerPosition2 - pos).sqrMagnitude) {

                    Debug.DrawLine(playerPosition2, pos, Color.white);
                    if (map.HasTile(gpos) && data.canBurn == true) {
                        if (activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                        SetTileOnFire(gpos, data);
                    }

            } else Debug.DrawLine(playerPosition2, pos, Color.red);
        }
    }

    public void BurnFromPlayerPositionDash()
    {
        //playerPosition2 = player.transform.position;
        //Vector3Int playergridPos = map.WorldToCell(playerPosition2);
        dashPosition2 = playerdashPosDown.transform.position;
        Vector3Int playergridPos = map.WorldToCell(dashPosition2);

        int gr = Mathf.FloorToInt(burnRadius + 0.5f);
        //var bounds = new BoundsInt(playergridPos, new Vector3Int(gr * 2 + 1, gr * 2 + 1, 1));
        bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);

        var rsq = burnRadius * burnRadius;

        foreach (var gpos in bounds.allPositionsWithin)
        {
            pos = (Vector2)map.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileData(gpos);
            if (rsq >= (dashPosition2 - pos).sqrMagnitude)
            {

                Debug.DrawLine(dashPosition2, pos, Color.white);
                if (map.HasTile(gpos) && data.softGround)
                {
                    print("tekeekö tää mitään");
                    if (activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    StartDestroyingSoftGround(gpos, data);
                    //SetTileOnFire(gpos, data);
                }
            }
            else Debug.DrawLine(dashPosition2, pos, Color.red);
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
                    if (activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    StartDestroyingSoftGround(gpos, data);
                    //SetTileOnFire(gpos, data);
                }
            }
            else Debug.DrawLine(dashPositionO, pos, Color.red);
        }
    }

    void BurnFromPlayerPositionMoving() {
        playerPosition2 = player.transform.position;
        Vector3Int playergridPos = mapMoving.WorldToCell(playerPosition2);

        int gr = Mathf.FloorToInt(burnRadius + 0.5f);
        //var bounds = new BoundsInt(playergridPos, new Vector3Int(gr * 2 + 1, gr * 2 + 1, 1));
        bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);

        var rsq = burnRadius * burnRadius;

        foreach (var gpos in bounds.allPositionsWithin) {
            pos = (Vector2)mapMoving.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileDataMoving(gpos);
            if (rsq >= (playerPosition2 - pos).sqrMagnitude) {

                Debug.DrawLine(playerPosition2, pos, Color.white);
                if (mapMoving.HasTile(gpos) && data.canBurn == true) {
                    if (activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    SetTileOnFireMoving(gpos, data);
                }
            } else Debug.DrawLine(playerPosition2, pos, Color.red);
        }
    }

    public void NewAshTile(Vector3Int tilePosition, GameObject pref) {

        GameObject ashT = Instantiate(pref);
        ashT.transform.position = map.GetCellCenterWorld(tilePosition);
        ashT.transform.SetParent(allFires.transform);

    }

    public void PlayFireSound() {
        
        AudioFW.AdjustVolume("FireBurningLoop", (float)activeFires.Count/30);
    }

    public void InstantiateBurnedPrefab(Vector3Int position) {
        TileData data = mapManager.GetTileData(position);

        if (!data.leavesTile) {
            map.SetTile(position, null);
            NewAshTile(position, data.burned);
        }

        StartCoroutine(InstantiateScorePrefab(position));

        if (scoreCounter) {
            scoreCounter.scoreValue += 1;
        }

        activeFires.Remove(position);
    }
    public void InstantiateBurnedPrefabMoving(Vector3Int position) {
        TileData data = mapManager.GetTileDataMoving(position);

        if (!data.leavesTile) {
            mapMoving.SetTile(position, null);
            NewAshTile(position, data.burned);
        }
        StartCoroutine(InstantiateScorePrefab(position));

        if (scoreCounter) {
            scoreCounter.scoreValue += 1;
        }

        activeFires.Remove(position);
    }

    public void FinishedBurning(Vector3Int position) {
        
        TileData data = mapManager.GetTileData(position);
        // käydään läpi secret efectit ja tuhotaan samassa kohdassa oleva samalla kun se poltetaan

        if(data.secret == true){
            foreach(var secr in secrets.ToArray()) {
                scoreCounter.secretValue += 1;
                if (secr.transform.position.x - 0.5f == position.x && secr.transform.position.y - 0.5f == position.y){
                    //var newpos
                    Destroy(secr);
                    secrets.Remove(secr);
                }
            }
        }
        StartCoroutine(InstantiateScorePrefab(position));
        SpawnSprite(position);


        if (data.canSmoke) {
            var randomNum = Random.Range(0, 3);
            if (randomNum == 1) {
                InstantiateSmoke(position);
            }
        }
        if (data.snowTile == true){
            //MeltedParticles(position);
        }
        else {
            BurnedParticles(position);
        }

        if (scoreCounter) {
            scoreCounter.scoreValue += 1;
        }

        if (data.leavesTile) {
            map.SetTile(position, null);

        }

        //if (!data.leavesTile) {
        //var idx = strings.IndexOf(data.name);
        //NewAshTile(position, prefabs[idx]);
        //NewAshTile(position, data.burned);
        //}
        activeFires.Remove(position);
    }

    public void FinishedBurningMoving(Vector3Int position) {
        
        TileData data = mapManager.GetTileDataMoving(position);

        if(data.secret == true){
            foreach(var secr in secrets.ToArray()){
                if(secr.transform.position.x - 0.5f == position.x && secr.transform.position.y - 0.5f == position.y){
                    scoreCounter.secretValue += 1;
                    Destroy(secr);
                    secrets.Remove(secr);
                    //tallennetaan tieto siitä, että salisuus löytyi

                }
            }
        }

        StartCoroutine(InstantiateScorePrefab(position));
        SpawnSprite(position);

        if (data.canSmoke) {
            var randomNum = Random.Range(0, 3);
            if (randomNum == 1) {
                InstantiateSmokeMoving(position);
            }
        }
        if (data.snowTile == true){
            //MeltedParticles(position);
        }
        else {
            BurnedParticles(position);
        }

        if (data.leavesTile) {
            mapMoving.SetTile(position, null);
        }

        if (scoreCounter) {
            scoreCounter.scoreValue += 1;
        }



        //if (!data.leavesTile) {
        //    NewAshTile(position, data.burned);
        //}
        activeFires.Remove(position);
    }

    void MeltedParticles( Vector3 posit ) {
        var EndParticleclone = Instantiate(meltedParticle,posit , transform.rotation);
        Destroy(EndParticleclone.gameObject, 1.5f);

    }

    void BurnedParticles( Vector3 posit ) {
        var EndParticleclone = Instantiate(burnedParticle,posit , transform.rotation);
        EndParticleclone.transform.SetParent(allFires.transform);
        Destroy(EndParticleclone.gameObject, 1.5f);

    }
    void BurnedParticleKpinäts(Vector3 posit)
    {
        var EndParticleclone = Instantiate(burnedParticleStart, posit, transform.rotation);
        EndParticleclone.transform.SetParent(allFires.transform);
        Destroy(EndParticleclone.gameObject, 1.1f);

    }

    void FireupParticles(Vector3 posit)
    {
        var EndParticleclone = Instantiate(fireupParticle, posit, transform.rotation);
        EndParticleclone.transform.SetParent(allFires.transform);
        Destroy(EndParticleclone.gameObject, 11f);

    }

    public void secretTileEffects(Vector3Int position){

        TileData data = mapManager.GetTileData(position);
        ParticleSystem secretParticle = Instantiate(data.secretParticle);
        
        secretParticle.transform.position = map.GetCellCenterWorld(position);

    }

    // t�� ainakin toimii :) mutta ei ehk� paras tapa
    public int GetTileAmountSprite() {
        int amount = 0;
        // loop through all of the tiles        
        BoundsInt bounds = map.cellBounds;
        BoundsInt bounds2 = mapMoving.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin) {
            TileData data = mapManager.GetTileData(pos);
            TileData data2 = mapManager.GetTileDataMoving(pos);
            Tile tile = map.GetTile<Tile>(pos);
            if (tile != null) {
                if (data.canBurn == true && data.secret == false) {
                    amount += 1;
                }
            }
        }

        foreach (Vector3Int pos in bounds2.allPositionsWithin) {
            TileData data2 = mapManager.GetTileDataMoving(pos);
            Tile tile = mapMoving.GetTile<Tile>(pos);
            if (tile != null) {
                if (data2.canBurn == true && data2.secret == false) {
                    amount += 1;
                }
            }
        }

        Debug.Log(amount);
        return amount;
    }

        public void SecretBurnableEffects() {

        // loop through all of the tiles        
        BoundsInt bounds = map.cellBounds;
        BoundsInt bounds2 = mapMoving.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin) {
            TileData data = mapManager.GetTileData(pos);

            Tile tile = map.GetTile<Tile>(pos);
            if (tile != null) {
                if (data.canBurn == true && data.secret == true) {
                    ParticleSystem secretParticle = Instantiate(data.secretParticle);
                    secretParticle.transform.SetParent(allFires.transform);
                    secretParticle.transform.position = map.GetCellCenterWorld(pos);
                    
                }
            }
        }

        foreach (Vector3Int pos in bounds2.allPositionsWithin) {
            TileData data2 = mapManager.GetTileDataMoving(pos);
            Tile tile = mapMoving.GetTile<Tile>(pos);
            if (tile != null) {
                if (data2.canBurn == true && data2.secret == true) {
                    ParticleSystem secretParticle = Instantiate(data2.secretParticle);
                    secretParticle.transform.SetParent(allFires.transform);
                    secretParticle.transform.position = map.GetCellCenterWorld(pos);
                }
            }
        }


    }

    public void InstantiateFireLights(Vector3Int tilePosition) {
        if (activeFires.Count <= 1) { 
        var lightTemp = Instantiate(fireLight);
        lightTemp.transform.SetParent(allFires.transform);
        lightTemp.transform.position = map.GetCellCenterWorld(tilePosition);
    } else {
            var randomNum = Random.Range(0, 2);
            if(randomNum == 1) {
                var lightTemp = Instantiate(fireLight);
                lightTemp.transform.SetParent(allFires.transform);
                lightTemp.transform.position = map.GetCellCenterWorld(tilePosition);
            }
        }

    }

    public void InstantiateSmoke(Vector3Int tilePosition) {
        TileData data = mapManager.GetTileData(tilePosition);
        GameObject smokeParticle = Instantiate(data.smokeParticle);

        smokeParticle.transform.position = map.GetCellCenterWorld(tilePosition);
        smokeParticle.transform.SetParent(allFires.transform);
    }

    public IEnumerator InstantiateScorePrefab( Vector3 tilePosition)
    {
        Vector2 randomOffset = new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f));
        Vector3 spawnPosition = tilePosition + new Vector3(randomOffset.x, randomOffset.y, 0f);
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        //var scoreprefab = Instantiate(scoreInsPrefab, spawnPosition, transform.rotation);
        //var textComponent = scoreprefab.GetComponentInChildren<TMP_Text>();

        //textComponent.text = scoreCounter.runningScore.ToString();

        //Destroy(scoreprefab.gameObject, 1f); // 
        // Running score tulee tästä, instantioi tuon
        yield return new WaitForSeconds(delay);
    }



    public void InstantiateSmokeMoving(Vector3Int tilePosition) {
        TileData data = mapManager.GetTileDataMoving(tilePosition);
        GameObject smokeParticle = Instantiate(data.smokeParticle);
        smokeParticle.transform.position = map.GetCellCenterWorld(tilePosition);
        smokeParticle.transform.SetParent(allFires.transform);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DashBox"))
        {
            Debug.Log("dashbox");
            BurnFromDashPosition();
        }
        if (collision.gameObject.CompareTag("DashDown"))
        {
            Debug.Log("dashboxdown");
            BurnFromDashPosition();
        }
    }

    public void BurnFromDashPosition()
    {
        Debug.Log("Starts burning from dash position");
        //var Position2 = playerdashPosDown.transform.position;
        //Vector3Int playergridPos = map.WorldToCell(Position2);
        playerPosition2 = player.transform.position;
        Vector3Int playergridPos = mapMoving.WorldToCell(playerPosition2);

        int gr = Mathf.FloorToInt(destRadius + 0.5f);
        //var bounds = new BoundsInt(playergridPos, new Vector3Int(gr * 2 + 1, gr * 2 + 1, 1));
        bounds = new BoundsInt(playergridPos.x - gr, playergridPos.y - gr, 0, gr * 2 + 1, gr * 2 + 1, 1);

        var rsq = destRadius * destRadius;


        foreach (var gpos in bounds.allPositionsWithin)
        {
            pos = (Vector2)map.CellToWorld(gpos) + Vector2.one * 0.5f;
            TileData data = mapManager.GetTileData(gpos);
            if (rsq >= (playerPosition2 - pos).sqrMagnitude)
            {
                print("entäs tähä");
                Debug.DrawLine(playerPosition2, pos, Color.blue);
                if (map.HasTile(gpos) && data.softGround == true)
                {
                    print("pääsi start destoying soft groundiin asti 11");
                    if (activeFires.Contains(gpos)) return; // ei sytytet� palavaa uudestaan
                    StartDestroyingSoftGround(gpos, data);

                    //SetTileOnFire(gpos, data);
                    print("pääsi start destoying soft groundiin asti");
                }
            }
            else Debug.DrawLine(playerPosition2, pos, Color.green);
        }


    }

    void StartDestroyingSoftGround(Vector3Int tilePosition, TileData data)
    {
        //map.SetColliderType(tilePosition, Tile.ColliderType.None);
        BurnedParticleKpinäts(tilePosition);

        Vector3Int tempTilepos = tilePosition;
        tempTilepos.y -= 1;
        softGroundNew = Instantiate(softGround);
        //softGround.transform.SetParent(allFires.transform);
        softGroundNew.transform.position = map.GetCellCenterWorld(tilePosition);
        softGroundNew.StartBurningGround(tilePosition, data, this);
        activeFires.Add(tilePosition);

        if (data.snowTile == true)
        {
            MeltedParticles(tilePosition);
        }

    }



    public void SpawnSprite(Vector3Int pos)
    {
        Vector3 spawnPosition = pos;
        //Vector3 spawnPosition = new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, targetPosition.transform.position.z);

        // Instantiate the object at the converted position
        Instantiate(spritePrefab, spawnPosition, Quaternion.identity);

        //Vector2 targetWorldPosition = UIUtilities.GetWorldPositionFromUI(canvas, uiTarget);
        ScoreBallsMovingToBar movementScript = spritePrefab.GetComponent<ScoreBallsMovingToBar>();
        if (movementScript != null)
        {
            //movementScript.targetPosition = targetWorldPosition;
            movementScript.uiTarget = uiTarget;
            movementScript.canvas = canvas;
        }
    }
}
