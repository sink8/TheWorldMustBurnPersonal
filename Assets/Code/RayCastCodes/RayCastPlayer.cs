using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RayCastPlayer : MonoBehaviour
{
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplayer = 2f;

    public float jumpHeight = 4f;
    public float timeToJumpApex = 0.5f;
    public float moveSpeed = 8;
    [SerializeField] float gravity = -8f;
    public float jumpVelocity = 10;
    float jumpVelocityDown = -310;

    float accelerationTimeGrounded = 0.15f;
    float accelerationTimeAirborne = 0.1f;

    float velocityXSmoothing;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumps = 0;
    Vector3 velocity;

    [Header("Dash Breaking")]
    [SerializeField] private LayerMask breakableLayer; 
    [SerializeField] private Vector2 dashCheckSize = new Vector2(1.2f, 1.2f); // Slightly larger than player

    [SerializeField]
    [Range(1f, 60f)] float dashSpeed;
    [SerializeField]
    [Range(1f, 60f)] float dashDownSpeed;
    [SerializeField]
    [Range(0f, 2f)] float dashTime;
    [SerializeField]
    [Range(0f, 2f)] float dashTimeDown;
    [SerializeField]
    [Range(0f, 2f)] float dashDistance = 2f;

    [SerializeField] float hangTime = 0.1f;

    public float hangTimeCounter;

    [SerializeField] float dashTimer = 0.5f;
    float timer = 0;
    bool canDash = true;

    public float startDashTime = 0.5f;
    public float startDashTimeDown = 0.5f;
    public int direction;
    //public bool isDashButtonDown;
    public bool dash = false;
    public bool dashDownVelocity = false;
    public bool inAirCurrent = false;
    private bool exitingAirCurrent;
    private float airCurrentExitTimer;

    public float airCurrentStrength = 5f;
    [SerializeField] float airCurrentDamping = 2f;   // Adjust this value for damping effect
    [SerializeField] float airCurrentExitDuration = 1f;  // Duration of the damping effect
    GameManager gm;

    RayCast2DController controller;
    

    public ParticleSystem kipinä;
    public ParticleSystem dashKipinä;
    public GameObject dashBlock;
    public GameObject dashBlockDown;
    PlayerHealth playerHealth;
    GameStart gameStart;

    float moveInputX;
    public float moveInputY;

    bool lookingRight, lookingLeft = false;
    
    // animation states


    public GameObject playerBodyRed, playerBodyBlue, playerBodyBlueReal, playerBodyPurple, playerBodyBlack, playerBodyGreen, playerBodyWhite;
    public ParticleSystem playerBodyRedParticle, playerBodyyellowParticle;
    public ParticleSystem eye1, eye2;
    public Color color2, color3, eyeColor;

    public InputManager inputManager;
    Vector2 moveInput;

    gamact1 _inputActions;

    SaveUI saveUI;
    public GameObject FireUI;
    private void OnEnable()
    {
        _inputActions = new gamact1();
        _inputActions.Gameplay.Enable();
        //_inputActions.map.Pause.started += OnPause;
        

        //_inputActions.map.Pause.started += ctx => _pressed = true;
        //_inputActions.map.Pause.canceled += ctx => _pressed = false;
    }

    private void OnDisable()
    {
        //_inputActions.map.Pause.started -= OnPause;
        
        _inputActions.Gameplay.Disable();

    }

    private void Start() {
        
        inputManager = InputManager.instance;

        gm = FindObjectOfType<GameManager>();
        saveUI = FindObjectOfType<SaveUI>();

        controller = GetComponent<RayCast2DController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        //gravity = -5.5f;
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print(" gravity " + gravity + " jump velocity " + jumpVelocity);

        dashTime = startDashTime;
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameStart = FindObjectOfType<GameStart>();

        if (saveUI.isPurple) {
            playerBodyRed.SetActive(false);
            playerBodyBlue.SetActive(false);
            playerBodyPurple.SetActive(true);
            playerBodyBlack.SetActive(false);
            playerBodyGreen.SetActive(false);

        }
        if (saveUI.isBlack) {
            playerBodyRed.SetActive(false);
            playerBodyBlue.SetActive(false);
            playerBodyPurple.SetActive(false);
            playerBodyBlack.SetActive(true);
            playerBodyGreen.SetActive(false);

        }
        if (saveUI.isGreen) {
            playerBodyRed.SetActive(false);
            playerBodyBlue.SetActive(false);
            playerBodyPurple.SetActive(false);
            playerBodyBlack.SetActive(false);
            playerBodyGreen.SetActive(true);

        }
        if (saveUI.isBlue) {
            playerBodyRed.SetActive(false);
            playerBodyBlueReal.SetActive(true);
            playerBodyPurple.SetActive(false);
            playerBodyBlack.SetActive(false);
            playerBodyGreen.SetActive(false);

        }
        if (saveUI.isWhite)
        {
            ToggleColorToWhite();

        }

    }

    private void Update() {
        moveInput = UserInput.instance.MoveInput;

        moveInputX = moveInput.x;
        moveInputY = moveInput.y;

        //moveInputX = Input.GetAxis("Horizontal");
        //moveInputY = Input.GetAxis("Vertical");

        Vector3 characterScale = transform.localScale;
        if (moveInputX < 0) {
            characterScale.x = -1;
            lookingLeft = true;
            lookingRight = false;
        }
        if (moveInputX > 0) {
            characterScale.x = 1;
            lookingRight = true;
            lookingLeft = false;
        }
        transform.localScale = characterScale;
        if(saveUI.colorChanged == false) {

            if (gm.State == PowerupType.None) {
                FireUI.SetActive(false);
                playerBodyRed.SetActive(true);
                playerBodyBlue.SetActive(false);
                playerBodyPurple.SetActive(false);
            } else if (gm.State == PowerupType.Projectile) {
                FireUI.SetActive(true);
                playerBodyRed.SetActive(false);
                playerBodyBlue.SetActive(true);
                playerBodyPurple.SetActive(false);
            } else if (gm.State == PowerupType.NoFire) {
                playerBodyRed.SetActive(false);
                playerBodyBlue.SetActive(false);
                playerBodyPurple.SetActive(true);
            }

        }

        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        

        if ((UserInput.instance.JumpJustPressed && controller.collisions.below))
        {
            //if (((inputManager.GetKeyDown(KeybindingActions.Jump) && controller.collisions.below)) || ((Input.GetButtonDown("Jump") && controller.collisions.below) )) {
            CreateKipinä();
            velocity.y = jumpVelocity;
        }

        if (controller.collisions.below == true)
        {
            hangTimeCounter = hangTime;
            jumps = 0;
        }


        //if (Input.GetButtonDown("Jump") && jumps < maxJumps) {
        if ((UserInput.instance.JumpJustPressed) && ((jumps < maxJumps) || hangTimeCounter > 0f))
        {
            //Debug.Log(" jump pressed");
            //if ((inputManager.GetKeyDown(KeybindingActions.Jump) && ((jumps < maxJumps) || hangTimeCounter > 0f)) || (Input.GetButtonDown("Jump") && ((jumps < maxJumps) || hangTimeCounter > 0f))) {
            AudioFW.Play("SwushLong");
            jumps++;
            velocity = Vector2.zero;
            velocity.y = jumpVelocity;
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            CreateKipinä();
            hangTimeCounter = 0;
        }

        if(jumps == 0)
        {
            playerBodyRedParticle.startColor = Color.white;
            playerBodyyellowParticle.startColor = Color.white;

        }

        if (jumps == 1)
        {
            playerBodyRedParticle.startColor = color2;
            playerBodyyellowParticle.startColor = color2;


        }

        if (jumps == maxJumps)
        {
            playerBodyRedParticle.startColor = color3;
            playerBodyyellowParticle.startColor = color3;

        }

        controller.Move(velocity * Time.deltaTime);

        Dash();
        DashDown();

        if (UserInput.instance.JumpJustPressed) Debug.Log("Jump triggered");
        if (UserInput.instance.DashInput) Debug.Log("Dash triggered");



    }

    private void FixedUpdate()
    {
        //var moveInput = UserInput.instance.MoveInput;


        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            dashDownVelocity = false;
        }




        //float targetVelocityX = input.x * moveSpeed;
        float targetVelocityX = moveInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.y += gravity * Time.deltaTime;

        // Apply gravity with different multipliers for jumping and falling

        if (velocity.y < 0)
        {
            if (dash == false && dashDownVelocity == false)
                velocity.y = jumpVelocityDown * Time.fixedDeltaTime; // Use a smaller fall multiplier (e.g., half)
        }


        if (inAirCurrent)
        {
            velocity.y = airCurrentStrength;  // Constant rising speed in air current
        }
        else
        {
            if (exitingAirCurrent)
            {
                airCurrentExitTimer -= Time.fixedDeltaTime;
                if (airCurrentExitTimer > 0)
                {
                    velocity.y = Mathf.Lerp(velocity.y, 0, Time.fixedDeltaTime * airCurrentDamping);
                }
                else
                {
                    exitingAirCurrent = false;
                }
            }
            else
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (velocity.y < 0)
            {
                velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.fixedDeltaTime;
                hangTimeCounter -= Time.fixedDeltaTime;
            }
            //else if ((velocity.y > 0 && !UserInput.instance.JumpJustPressed) || (velocity.y > 0 && (!Input.GetButtonDown("Jump"))))
            else if ((velocity.y > 0 && !UserInput.instance.JumpJustPressed))
            {
                velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.fixedDeltaTime;
            }
        }

        //controller.Move(velocity * Time.fixedDeltaTime);


    }

    void CreateKipinä() {
        kipinä.Play();
    }
    void CreateDashKipinä() {
        dashKipinä.Play();
    }




    void Dash()
    {
        if (direction == 0)
        {
            //if (((inputManager.GetKeyDown(KeybindingActions.Dash) || Input.GetButton("Dash")) && canDash == true) )
              if (((UserInput.instance.DashInput ) && canDash == true) )
            {
                AudioFW.Play("SwushShort");
                //if (moveInputX < 0)
                if (lookingLeft == true)
                    {
                    CreateDashKipinä();
                    direction = 1;
                    dash = true;
                    dashBlock.SetActive(true);
                }
                //else if (moveInputX > 0)
                else if (lookingRight == true)
                        {
                    CreateDashKipinä();
                    direction = 2;
                    dash = true;
                    dashBlock.SetActive(true);
                }
                canDash = false;
                ParticleSystem.MainModule psmaineye1 = eye1.main;
                ParticleSystem.MainModule psmaineye2 = eye2.main;

                psmaineye1.startColor = eyeColor;
                psmaineye2.startColor = eyeColor;
                StartCoroutine(DashTimer());
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                dash = false;
                direction = 0;
                dashTime = startDashTime;
                velocity = Vector2.zero;
                dashBlock.SetActive(false);
            }
            else
            {
                dashTime -= Time.deltaTime;

                // Perform the break check every frame while dashing
                CheckForBreakables();

                if (direction == 1) { velocity = Vector2.left * dashSpeed; } else if (direction == 2) { velocity = Vector2.right * dashSpeed; }
            }
        }
        if (dash == false)
        {
            dashBlock.SetActive(false);
        }
    }

    void DashDown()
    {
        if (direction == 0)
        {
            //if ((UserInput.instance.DashDownInput || moveInputY < -0.1f) && canDash == true)
            if ((UserInput.instance.DashDownInput) && canDash == true)
                {
                AudioFW.Play("SwushShort");
                CreateDashKipinä();
                direction = 3;
                dash = true;
                canDash = false;
                StartCoroutine(DashTimer());
                dashDownVelocity = true;
            }
        }
        else
        {
            if (dashTimeDown <= 0)
            {
                dash = false;
                direction = 0;
                dashTimeDown = startDashTimeDown;
                velocity = Vector2.zero;
                dashBlockDown.SetActive(false);
                dashDownVelocity = true;
            }
            else
            {
                dashTimeDown -= Time.deltaTime;

                if (direction == 3)
                {
                    dashBlockDown.SetActive(true);
                    //velocity = Vector2.down * dashDownSpeed;
                    //dashDownVelocity = true;
                    CheckForBreakables();
                    velocity = Vector2.down * dashDownSpeed;
                    dashDownVelocity = true;
                }

            }
        }

        if (dash == false)
        {
            dashBlockDown.SetActive(false);
        }
    }

    void CheckForBreakables() {
        // Use the player's position and dash size
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, dashCheckSize, 0, breakableLayer);

        foreach (Collider2D hit in hits) {
            // IMPORTANT: Use the tag you've assigned to your tiles
            if (hit.CompareTag("Ash")) {
                if (hit.TryGetComponent<AshTilesColliding>(out var ash)) {
                    AudioFW.Play("HitWood");
                    ash.BreakTile(); // This starts the chain reaction
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AirCurrent"))
        {
            inAirCurrent = true;
            velocity.y = airCurrentStrength;
            exitingAirCurrent = false;
            jumps = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AirCurrent"))
        {
            inAirCurrent = false;
            exitingAirCurrent = true;
            airCurrentExitTimer = airCurrentExitDuration;
        }
        
    }

    private IEnumerator DashTimer() {
        yield return new WaitForSeconds(1f);
        ParticleSystem.MainModule psmaineye1 = eye1.main;
        ParticleSystem.MainModule psmaineye2 = eye2.main;

        psmaineye1.startColor = Color.white;
        psmaineye2.startColor = Color.white;
        canDash = true;
        dashTime = startDashTime;
        dashTimeDown = startDashTimeDown;
    }

    public void SwitchColorTo(string color) {

    }

    public void ToggleColorToBlue() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(true);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(false);
    }
    public void ToggleColorToBlueReal() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(false);
        playerBodyBlueReal.SetActive(true);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(false);
    }
    public void ToggleColorToPurple() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(false);
        playerBodyPurple.SetActive(true);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(false);
    }
    public void ToggleColorToBlack() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(false);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(true);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(false);
    }
    public void ToggleColorToGreen() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(false);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(true);
        playerBodyWhite.SetActive(false);
    }
    public void ToggleColorToWhite() {
        playerBodyRed.SetActive(false);
        playerBodyBlue.SetActive(false);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(true);
    }
    public void ToggleColorToRed() {
        playerBodyRed.SetActive(true);
        playerBodyBlue.SetActive(false);
        playerBodyPurple.SetActive(false);
        playerBodyBlack.SetActive(false);
        playerBodyGreen.SetActive(false);
        playerBodyWhite.SetActive(false);
    }

} // class
