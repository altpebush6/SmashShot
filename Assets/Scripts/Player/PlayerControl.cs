using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PlayerControl : MonoBehaviourPunCallbacks
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform canvasTF;
    [SerializeField] private Transform[] groundChecks;
    [SerializeField] private float groundCheckDistance; 

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool canDoubleJump;
    private bool lookLeft;

    private PhotonView photonView;

    private GameManager GM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        photonView = GetComponent<PhotonView>();

        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        if(photonView.IsMine)
        {
            animator = GetComponent<Animator>();

            GM = GameObject.Find("GameManager").GetComponent<GameManager>();

            canDoubleJump = true;
            lookLeft = true;
        }
        else
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if(photonView.IsMine)
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
    }

    void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");     

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        
        if((horizontal > 0f && lookLeft) || (horizontal < 0f && !lookLeft))
        {
            Vector3 scale = gameObject.transform.localScale;
            scale.x *= -1f;
            gameObject.transform.localScale = scale;

            Vector3 canvasScale = canvasTF.localScale;
            canvasScale.x *= -1f;
            canvasTF.localScale = canvasScale;

            lookLeft = !lookLeft;
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
        bool checkRay = false;
        foreach(Transform groundCheck in groundChecks)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);

            if(hit.collider != null)
            {
                checkRay = true;
            }
        }

        isGrounded = checkRay;

        animator.SetBool("isGrounded", isGrounded);
    }
}
