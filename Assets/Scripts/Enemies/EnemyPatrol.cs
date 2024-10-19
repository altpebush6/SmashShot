using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    private Transform currentPoint;

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentPoint = pointB.transform;

        animator.SetBool("isRunning", true);
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if(currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = (currentPoint == pointA.transform) ? pointB.transform : pointA.transform;
            
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
