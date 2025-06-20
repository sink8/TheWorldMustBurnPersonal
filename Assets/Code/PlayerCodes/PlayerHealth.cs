using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private MapManager mapManager;
    RayCastPlayer castPlayer;

    public Tilemap map;
    public Tilemap mapMoving;

    public GameObject player;
    public GameObject playerParticles;
    public int health = 1;
    public float timer = 1f;

    public MenuNavigation menuNav;
    RayCastPlayer playerController;
    public Transform DeathParticle;

    public Image fadeIm;
    public Animator anim;
    bool fadebool = false;
    PlayLoops playLoops;

    public bool respawnActivated = false;
    Vector3 reSpawnpoint;

    public ParticleSystem preCheckPoint;
    public ParticleSystem savedCheckPoint;
    PlayerHealth playerHealth;
    BoxCollider2D collider2D;
    

    public Animator animat;

    void Start() {
        //DeathUI = GameObject.Find("Menu");
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
        mapMoving = GameObject.FindGameObjectWithTag("MovingMap").GetComponent<Tilemap>();
        castPlayer = gameObject.GetComponent<RayCastPlayer>();


        menuNav = FindObjectOfType<MenuNavigation>();
        playerController = FindObjectOfType<RayCastPlayer>();
        playLoops = FindObjectOfType<PlayLoops>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        collider2D = gameObject.GetComponent<BoxCollider2D>();
        //playerParticles = GameObject.Find("AllPlayerParticles");

        //fade = FindObjectOfType<FadePanel>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchWaterY(1);
        if(health == 0) {
            //playerParticles.SetActive(false);
            if (timer > 0) {
                timer -= Time.deltaTime;
                if (timer <= 0) {
                    timer = 0;
                    //menuNav.FadeDeathPanel();
                }
            }
        }
    }
    public void Damaged(int damage) {
        
        health -= damage;
        castPlayer.enabled = false;
        if (health >= 1 && fadebool == false)
        {
            //animat.Play("dissolve_fade");
            AudioFW.Play("Death");
            DestroySpark();
            StopAllParticlesInFireParent(playerParticles);
            playerHealth.enabled = false;
            collider2D.enabled = false;
            fadebool = true;
            StartCoroutine(WaitTillRespawn());
            //} else
            //{
            //    playLoops.StopLevelMusic();
            //    health = 0;
            //    // kuolema animaatio
            //    if (fadebool == false) {
            //        DestroySpark();
            //        fadebool = true;
            //        AudioFW.Play("Death");
            //    }
            //    player.GetComponent<RayCastPlayer>().enabled = false;
            //    //playerController.DeathAnim();
            //    //anim.Play("FadeIn");
            //    //anim.Play("FadeOut");
            //    Destroy(transform.parent.gameObject, 1.5f);
            //    menuNav.OpenDeathMenu();

            //}
        }
    }

    IEnumerator WaitTillRespawn()
    {
        yield return new WaitForSeconds(1.5f);
        castPlayer.enabled = true;
        player.transform.position = reSpawnpoint;
        StartAllParticlesInFireParent(playerParticles);
        playerHealth.enabled = true;
        collider2D.enabled = true;
        fadebool = false;

    }

    public void DestroyLevel() {
        Destroy(transform.parent.gameObject);
    }

    public void StopAllParticlesInFireParent(GameObject fireParent)
        {
            ParticleSystem[] particleSystems = fireParent.GetComponentsInChildren<ParticleSystem>();

            foreach (var ps in particleSystems)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

     public void StartAllParticlesInFireParent(GameObject fireParent)
        {
            ParticleSystem[] particleSystems = fireParent.GetComponentsInChildren<ParticleSystem>();

            foreach (var ps in particleSystems)
            {
                ps.Play(true);
            }
        }

    // damege tarvii my�s timerin jos voi ottaa enemm�n kuin yhden damagen
    void TouchWaterY(int damage) {
        Vector2 playerPosition = player.transform.position;
        Vector3Int playergridPos = map.WorldToCell(playerPosition);
        Vector3Int playergridPosMoving = mapMoving.WorldToCell(playerPosition);
        playergridPos.y -= 1;
        playergridPosMoving.y -= 1;

        TileData data = mapManager.GetTileData(playergridPos);
        TileData dataMoving = mapManager.GetTileData(playergridPosMoving);
        if (map.HasTile(playergridPos) && data.waterTile == true) {
            Damaged(damage);
        }
        if (map.HasTile(playergridPosMoving) && dataMoving.waterTile == true) {
            Damaged(damage);
            print("osui veteen");
        }
    }

    void ReSpawnPoint()
    {
        AudioFW.Play("Checkpoint");
        //respawnActivated = true;
        //if(respawnActivated == false)
        //{
            respawnActivated = true;
            savedCheckPoint.Play();
            preCheckPoint.Stop();
            //savedCheckPoint.SetActive(true);
            //preCheckPoint.SetActive(false);

        //}

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Water")){
            AudioFW.Play("Death");
            Damaged(1);
        }

        //print("osui");
        if (collision.gameObject.CompareTag("Respawn"))
        {
            print("player osui");

            Transform savedChild = collision.transform.Find("saved");
            Transform preChild = collision.transform.Find("pre");

            if (savedChild != null)
            {
                ParticleSystem particsaved = savedChild.GetComponent<ParticleSystem>();
                if (particsaved != null)
                {
                    savedCheckPoint = particsaved;
                }
                else
                {
                    Debug.Log("No particle system found on the 'saved' object in Respawn");
                }
            }
            else
            {
                Debug.Log("No 'saved' child object found in Respawn");
            }

            if (savedChild != null)
            {
                ParticleSystem particpre = preChild.GetComponent<ParticleSystem>();
                if (particpre != null)
                {
                    preCheckPoint = particpre;
                }
                else
                {
                    Debug.Log("No particle system found on the 'saved' object in Respawn");
                }
            }

            ReSpawnPoint();
            reSpawnpoint = collision.ClosestPoint(transform.position);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Water")){
            
            AudioFW.Play("Death");
            Damaged(1);
        }


    }




    void DestroySpark() {
        var projectileEndParticleclone = Instantiate(DeathParticle, transform.position, transform.rotation);
        Destroy(projectileEndParticleclone.gameObject, 2);
        //Destroy(projectileEndParticleclone);
    }


    IEnumerator FadingImage() {
        yield return null;

    }


    public void FadeInanim() {
            anim.Play("FadeIn");

    }
    public void FadeOut() {
        anim.Play("FadeOut");

    }
}
