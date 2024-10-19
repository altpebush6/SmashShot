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

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool canDoubleJump;

    private GameManager GM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        canDoubleJump = true;
    }

    void Update()
    {
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
        }
    }

    void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");        

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
    }
}
