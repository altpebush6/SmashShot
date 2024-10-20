using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer; 

    [SerializeField] private AudioSource jumpAudio; 
    [SerializeField] private AudioSource runAudio; 
    private bool runAudioPlay;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool canDoubleJump;

    private GameManager GM;

    [HideInInspector] public bool portalPassed; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        canDoubleJump = true;
        runAudioPlay = false;
        portalPassed = false;
    }

    void Update()
    {
        if(portalPassed)
        {
            StopRunAudio();
            gameObject.SetActive(false);
        }

        canDoubleJump = (isGrounded) ? true : canDoubleJump;

        if(GM.isGameActive())
        {
            GroundCheck();
            Move();
            Jump();
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            StopRunAudio();
        }
    }

    void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");     

        if(horizontal != 0f)
        {
            StartRunAudio();
        }
        else
        {
            StopRunAudio();
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        if(horizontal > 0f)
        {
            sr.flipX = true;
        }
        else if(horizontal < 0f)
        {
            sr.flipX = false;
        }

        animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));
    }

    void Jump()
    {
        if(Input.GetButtonDown("Up"))
        {
            if(canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpAudio.Play();

                if(!isGrounded)
                {
                    canDoubleJump = false;
                }
            }
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        animator.SetBool("isGrounded", isGrounded);

        if(!isGrounded)
        {
            StopRunAudio();
        }
    }

    void StartRunAudio()
    {
        if(!runAudioPlay && isGrounded)
        {
            runAudio.Play();
            runAudioPlay = true;
        }
    }
    void StopRunAudio()
    {
        if(runAudioPlay)
        {
            runAudio.Stop();
            runAudioPlay = false;
        }
    }
}
