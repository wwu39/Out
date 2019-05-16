using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DashState  { NotDashing, Left, Right }

public class PlayerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string WalkSound;
    [FMODUnity.EventRef]
    public string JumpSound;
    [FMODUnity.EventRef]
    public string DashSound;
    [FMODUnity.EventRef]
    public string DieSound;
    FMOD.Studio.EventInstance WalkSoundEvent;


    Rigidbody rb;
    public GameObject PlayerShatter;

    public bool isDead = false;

    bool isGrounded;
    bool isJumping;
    LayerMask layerMask;
    float distToGrounded = 0.2f;
    public float jumpHeight = 350;
    public float moveSpeed = 200;
    public float dashSpeed = 500;
    float playerJumpingUpGravity = 1f;
    float playerJumpingDownGravity = 10f;
    float maxDownVelocity = 30f;

    Animation anim;
    TrailRenderer trailer;

    // dash
    bool firstKeyPress = false;
    bool readyForSecondKeyPress = false;
    float keyPressDelay = 0.3f;
    float keyPressDelayCount = -1;
    float keyPressThreshold = 0.1f;
    DashState dashDir = DashState.NotDashing;
    DashState dashingDir = DashState.NotDashing;
    bool isDashing  = false;
    float dashTime = -1;
    public float dashDuration = 0.1f;
    float dashCooldownStart = -1;
    public float dashCooldown = 0.5f;
    bool PlayDashing= false;
    bool PlayAirDashing = false;

    CapsuleCollider playerCollider;

    private void CheckDash() // double arrow key to dash
    {
        if (isDashing || dashCooldownStart >= 0) return;

        void vain()
        {
            firstKeyPress = false;
            readyForSecondKeyPress = false;
            keyPressDelayCount = -1;
            dashDir = DashState.NotDashing;
        }
        float h_input = Input.GetAxis("Horizontal");
        if (Mathf.Abs(h_input) > keyPressThreshold && !firstKeyPress)
        {
            if (h_input > 0) dashDir = DashState.Right; else dashDir = DashState.Left;
            firstKeyPress = true;
        }
        if (firstKeyPress && Mathf.Abs(h_input) <= keyPressThreshold && !readyForSecondKeyPress)
        {
            keyPressDelayCount = Time.time;
            readyForSecondKeyPress = true;
        }
        if (readyForSecondKeyPress)
        {
            if (keyPressDelayCount > 0 && Time.time - keyPressDelayCount > keyPressDelay) vain();
            else
            {
                switch (dashDir)
                {
                    case DashState.Left:
                        if (h_input < -keyPressThreshold)
                        {
                            isDashing = true;
                            dashingDir = DashState.Left;
                            dashTime = Time.time;
                            trailer.enabled = true;
                            vain();
                        }
                        else if (h_input > keyPressThreshold) vain();
                        break;
                    case DashState.Right:
                        if (h_input > keyPressThreshold)
                        {
                            isDashing = true;
                            dashingDir = DashState.Right;
                            dashTime = Time.time;
                            trailer.enabled = true;
                            vain();
                        }
                        else if (h_input < -keyPressThreshold) vain();
                        break;
                }
            }
        }
    }

    void CheckDash2() // shift to dash
    {
        if (isDashing || dashCooldownStart >= 0) return;
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isDashing = true;
            dashingDir = transform.forward.x > 0 ? DashState.Right : DashState.Left;
            dashTime = Time.time;
            trailer.enabled = true;
            if (isGrounded)
            {
                PlayDashing = true;
                anim.Play("Dashing");
                halveCollider();
            }
            else
            {
                PlayAirDashing = true;
                anim.Play("AirDashing");
            }
            FMODUnity.RuntimeManager.PlayOneShot(DashSound);
        }
    }

    private void GravityModifier()
    {
        if (rb.velocity.y > 0.1f) //Travelling Up
        {
            rb.velocity += Vector3.down * (playerJumpingUpGravity - 1) * Time.fixedDeltaTime;
        }

        else if (rb.velocity.y < -maxDownVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, -maxDownVelocity, 0);
        }

        else if (rb.velocity.y < -0.1f) //Travelling down
        {
            rb.velocity += Vector3.down * (playerJumpingDownGravity - 1) * Time.fixedDeltaTime;
        }
    }

    void halveCollider()
    {
        playerCollider.direction = 2;
        playerCollider.center = new Vector3(0, playerCollider.radius, 0);
    }

    void doubleCollider()
    {
        playerCollider.direction = 1;
        playerCollider.center = new Vector3(0, 7.197668f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        // RigidBody
        rb = GetComponent<Rigidbody>();

        // collider
        playerCollider = GetComponent<CapsuleCollider>();

        // Animations
        anim = GetComponent<Animation>();
        anim["Dashing"].speed = 10;

        // Can only jump on tile and crate
        string[] layers = { "Tile", "sTile", "Crate", "TileLike", "Default" };
        layerMask = LayerMask.GetMask(layers);

        // Dashing trailer
        trailer = GetComponent<TrailRenderer>();

        WalkSoundEvent = FMODUnity.RuntimeManager.CreateInstance(WalkSound);
    }

    // Update is called once per frame
    void Update()
    {
        // cheack ground
        Vector3 start = transform.position;
        start.y += 0.3f;
        if (Physics.Raycast(start, Vector3.down, distToGrounded + 0.1f, layerMask)) isGrounded = true;
        else isGrounded = false;

        // check dash
        if (dashCooldownStart >= 0 && Time.time - dashCooldownStart > dashCooldown) dashCooldownStart = -1;
        // CheckDash(); // Double arrow key to dash
        CheckDash2(); // shift to dash

        // check jumping
        if (isJumping && rb.velocity.y < 0)
        {
            isJumping = false;
            anim.Play("JumpDown");
        }


        // handle animation and direction
        if (rb.velocity.magnitude > 0.1f && !isJumping && !isDashing && !anim.isPlaying)
        {
            anim.Play("Walk");
            FMOD.Studio.PLAYBACK_STATE state;
            WalkSoundEvent.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING && isGrounded) WalkSoundEvent.start();
        }
        var dir = new Vector3();
        dir.x = rb.velocity.x;
        if (dir.magnitude > 0.1f) transform.rotation = Quaternion.LookRotation(dir);

        // handle death
        if (isDead)
        {
            FMODUnity.RuntimeManager.PlayOneShot(DieSound);
            var deadbody = Instantiate(PlayerShatter) as GameObject;
            deadbody.transform.position = transform.position;
            deadbody.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
            isDead = false;
        }
    }

    private void FixedUpdate()
    {
        GravityModifier();
        if (GameManager.ins.allowedInput && !isDead)
        {
            if (!isDashing)
            {
                // handle movement
                var movement = Input.GetAxis("Horizontal");
                if (Mathf.Abs(movement) > 0.1f)
                {
                    if (movement > 0) rb.velocity = new Vector3(moveSpeed * Time.fixedDeltaTime, rb.velocity.y, 0);
                    else rb.velocity = new Vector3(-moveSpeed * Time.fixedDeltaTime, rb.velocity.y, 0);
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }

                // handle jump
                if ((Input.GetAxis("Vertical") > 0 || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
                {
                    isJumping = true;
                    anim.Play("JumpUp");
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // reset force.y
                    rb.AddForce(transform.up * jumpHeight * Time.fixedDeltaTime, ForceMode.Impulse); // jump
                    FMODUnity.RuntimeManager.PlayOneShot(JumpSound);
                }
            }
            else
            {
                // handle dashing
                if (Time.time - dashTime > dashDuration)
                {
                    isDashing = false;
                    rb.velocity = Vector3.zero;
                    dashingDir = DashState.NotDashing;
                    trailer.enabled = false;
                    dashTime = -1;
                    dashCooldownStart = Time.time;
                    if (PlayDashing)
                    {
                        anim.Play("FinishDashing");
                        doubleCollider();
                        PlayDashing = false;
                    }
                    else if (PlayAirDashing)
                    {
                        anim.Play("FinishAirDashing");
                        PlayAirDashing = false;
                    }
                }

                var dashingVelocity = Vector3.zero;
                switch (dashingDir)
                {
                    case DashState.Left:
                        dashingVelocity.x = -2000 * Time.fixedDeltaTime;
                        break;
                    case DashState.Right:
                        dashingVelocity.x = 2000 * Time.fixedDeltaTime;
                        break;
                }
                rb.velocity = dashingVelocity;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "DashingExtend")
        {
            if (isDashing && dashCooldownStart >= 0 && Time.time - dashCooldownStart > dashCooldown - 0.2)
            {

                print("A");
                dashCooldownStart += 0.2f;
            }
        }
    }
}
