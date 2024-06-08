using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastPlayer : MonoBehaviour
{
    public float fallMultiplayer = 2.5f;
    public float lowJumpMultiplayer = 2f;

    public float jumpHeight = 4f;
    public float timeToJumpApex = 0.5f;
    public float moveSpeed = 8;
    float gravity = -8f;
    public float jumpVelocity = 10;
    float accelerationTimeGrounded = 0.15f;
    float accelerationTimeAirborne = 0.1f;

    float velocityXSmoothing;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumps = 0;
    Vector3 velocity;

    [SerializeField]
    [Range(1f, 60f)] float dashSpeed;
    [SerializeField]
    [Range(1f, 60f)] float dashDownSpeed;
    [SerializeField]
    [Range(0f, 2f)] float dashTime;
    [SerializeField]
    [Range(0f, 2f)] float dashTimeDown;
    [SerializeField] float hangTime = 0.1f;

    float hangTimeCounter;

    [SerializeField] float dashTimer = 0.5f;
    float timer = 0;
    bool canDash = true;

    public float startDashTime;
    public float startDashTimeDown;
    public int direction;
    public bool isDashButtonDown;
    public bool dash = false;
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


    // animation states


    public GameObject playerBodyRed, playerBodyBlue, playerBodyPurple;

    public InputManager inputManager;

    private void Start() {
        inputManager = InputManager.instance;

        gm = FindObjectOfType<GameManager>();

        controller = GetComponent<RayCast2DController>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print(" gravity " + gravity + " jump velocity " + jumpVelocity);

        dashTime = startDashTime;
        playerHealth = FindObjectOfType<PlayerHealth>();
        gameStart = FindObjectOfType<GameStart>();

    }

    private void Update() {

        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0) {
            characterScale.x = -1;
        }
        if (Input.GetAxis("Horizontal") > 0) {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;

        if (gm.State == PowerupType.None) {
            playerBodyRed.SetActive(true);
            playerBodyBlue.SetActive(false);
            playerBodyPurple.SetActive(false);
        } else if (gm.State == PowerupType.Projectile) {
            playerBodyRed.SetActive(false);
            playerBodyBlue.SetActive(true);
            playerBodyPurple.SetActive(false);
        } else if (gm.State == PowerupType.NoFire) {
            playerBodyRed.SetActive(false);
            playerBodyBlue.SetActive(false);
            playerBodyPurple.SetActive(true);
        }


        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        var moveInput = UserInput.instance.MoveInput;

        if ((UserInput.instance.JumpJustPressed && controller.collisions.below) ) {
            //if (((inputManager.GetKeyDown(KeybindingActions.Jump) && controller.collisions.below)) || ((Input.GetButtonDown("Jump") && controller.collisions.below) )) {
            CreateKipinä();
            velocity.y = jumpVelocity;
        }

        if (controller.collisions.below == true) {
            hangTimeCounter = hangTime;
            jumps = 0;
        }

        //if (Input.GetButtonDown("Jump") && jumps < maxJumps) {
        if ((UserInput.instance.JumpJustPressed) && ((jumps < maxJumps) || hangTimeCounter > 0f)) {
            //if ((inputManager.GetKeyDown(KeybindingActions.Jump) && ((jumps < maxJumps) || hangTimeCounter > 0f)) || (Input.GetButtonDown("Jump") && ((jumps < maxJumps) || hangTimeCounter > 0f))) {
            AudioFW.Play("SwushLong");
            jumps++;
            velocity = Vector2.zero;
            velocity.y = jumpVelocity;
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            CreateKipinä();
            hangTimeCounter = 0;
        }


        //float targetVelocityX = input.x * moveSpeed;
        float targetVelocityX = moveInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        //velocity.y += gravity * Time.deltaTime;


        if (inAirCurrent)
        {
            velocity.y = airCurrentStrength;  // Constant rising speed in air current
        }
        else
        {
            if (exitingAirCurrent)
            {
                airCurrentExitTimer -= Time.deltaTime;
                if (airCurrentExitTimer > 0)
                {
                    velocity.y = Mathf.Lerp(velocity.y, 0, Time.deltaTime * airCurrentDamping);
                }
                else
                {
                    exitingAirCurrent = false;
                }
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            if (velocity.y < 0)
            {
                velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
                hangTimeCounter -= Time.deltaTime;
            }
            //else if ((velocity.y > 0 && !UserInput.instance.JumpJustPressed) || (velocity.y > 0 && (!Input.GetButtonDown("Jump"))))
            else if ((velocity.y > 0 && !UserInput.instance.JumpJustPressed))
            {
                velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplayer - 1) * Time.deltaTime;
            }
        }

        controller.Move(velocity * Time.deltaTime);

        Dash();
        DashDown();
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
                if (moveInputX < 0)
                {
                    CreateDashKipinä();
                    direction = 1;
                    dash = true;
                    //dashBlock.SetActive(true);
                }
                else if (moveInputX > 0)
                {
                    CreateDashKipinä();
                    direction = 2;
                    dash = true;
                    //dashBlock.SetActive(true);
                }
                canDash = false;
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

                if (direction == 1)
                {
                    dashBlock.SetActive(true);
                    velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    dashBlock.SetActive(true);
                    velocity = Vector2.right * dashSpeed;
                }
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
            if ((UserInput.instance.DashDownInput || moveInputY < -0.1f) && canDash == true)
            {
                AudioFW.Play("SwushShort");
                CreateDashKipinä();
                direction = 3;
                dash = true;
                canDash = false;
                StartCoroutine(DashTimer());
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                dash = false;
                direction = 0;
                dashTime = startDashTimeDown;
                velocity = Vector2.zero;
                dashBlockDown.SetActive(false);
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 3)
                {
                    dashBlockDown.SetActive(true);
                    velocity = Vector2.down * dashDownSpeed;
                }

            }
        }

        if (dash == false)
        {
            dashBlockDown.SetActive(false);
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
        canDash = true;
    }

} // class
