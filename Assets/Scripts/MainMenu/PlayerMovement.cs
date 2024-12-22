using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float stepSize;
    [SerializeField] private float waitTime;

    [SerializeField] private GameObject ball;
    [SerializeField] private float ballVelocity = 0.01f;  // Initial velocity

    [SerializeField] private float ballGravityMultiplier = 0.005f;  // Acceleration factor (gravity effect)
    [SerializeField] private float maxDownwardVelocity = 0.2f;  // Maximum falling speed
    [SerializeField] private float maxUpwardVelocity = 0.01f;   // Minimum upward speed (starts slow)

    [SerializeField] private float PlayerMin;
    [SerializeField] private float PlayerMax;
    [SerializeField] private float BallMin;
    [SerializeField] private float BallMax;

    [SerializeField] private bool lookLeft;
    private bool goDown;

    void Start()
    {
        goDown = true;

        stepSize = -stepSize;

        StartCoroutine(Move());
        StartCoroutine(MoveBall());
    }

    void Update()
    {
        // Handling player movement
        if (transform.position.x < PlayerMin && lookLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            lookLeft = false;
            stepSize *= -1f;
        }
        else if (transform.position.x > PlayerMax && !lookLeft)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
            lookLeft = true;
            stepSize *= -1f;
        }

        // Ball velocity handling
        if (ball.transform.position.y < BallMin && goDown)
        {
            ballVelocity = Mathf.Abs(ballVelocity);  // Make sure the velocity is positive when going up
            goDown = false;
        }
        else if (ball.transform.position.y > BallMax && !goDown)
        {
            ballVelocity = -Mathf.Abs(ballVelocity);  // Make sure the velocity is negative when going down
            goDown = true;
        }
    }

    IEnumerator Move()
    {
        // Move the player horizontally
        transform.position = new Vector2(transform.position.x + stepSize, transform.position.y);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Move());
    }

    IEnumerator MoveBall()
    {
        // Simulate gravity-like effect
        if (goDown)
        {
            // Increase velocity while going down (gravity effect)
            ballVelocity -= ballGravityMultiplier;
            ballVelocity = Mathf.Clamp(ballVelocity, -maxDownwardVelocity, -0.01f);  // Limit downward velocity
        }
        else
        {
            // Decrease velocity while going up (against gravity)
            ballVelocity += ballGravityMultiplier;
            ballVelocity = Mathf.Clamp(ballVelocity, 0.01f, maxUpwardVelocity);  // Limit upward velocity
        }

        // Move the ball vertically with adjusted velocity
        ball.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + ballVelocity, ball.transform.position.z);

        // Small delay to maintain control over updates
        yield return new WaitForSeconds(0.01f);

        StartCoroutine(MoveBall());
    }
}
