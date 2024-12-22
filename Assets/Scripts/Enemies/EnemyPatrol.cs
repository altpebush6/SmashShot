using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private GameManager GM;

    private Transform pointA;
    private Transform pointB;
    private Transform currentPoint;

    [SerializeField] private float speed;
    [SerializeField] private Transform canvasTF;

    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        if(GM.isGameActive() && currentPoint != null)
        {
            Vector2 point = currentPoint.position - transform.position;

            if(currentPoint == pointB)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                currentPoint = (currentPoint == pointA) ? pointB : pointA;
                
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                canvasTF.localScale = new Vector3(canvasTF.localScale.x * -1, canvasTF.localScale.y, canvasTF.localScale.z);
            }
        }
    }

    public void SetPointA(Transform point) { pointA = point; }
    public void SetPointB(Transform point) { pointB = point; }
    public void SetCurrentPoint(Transform point) { currentPoint = point; }
}
