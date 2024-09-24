using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallM : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10f;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerOneScore;
    [SerializeField] private Text playerTwoScore;

    private int hitCounter;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disable gravity for the ball
        StartBall();  // Start the ball movement
    }

    void FixedUpdate()
    {
        // Clamp the ball's velocity to prevent it from going too fast
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        // Start the ball moving along the X-axis
        rb.velocity = new Vector3(-1, 0, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        // Reset ball position and velocity after a goal
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;  // Move the ball back to the center
        hitCounter = 0;  // Reset hit counter
        StartBall();  // Restart ball movement
    }

    private void PlayerBounce(Transform paddle)
    {
        hitCounter++;

        Vector3 ballPos = transform.position;
        Vector3 paddlePos = paddle.position;

        float xDirection = transform.position.x > 0 ? -1 : 1;
        float yDirection = (ballPos.y - paddlePos.y) / paddle.GetComponent<Collider>().bounds.size.y;

        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        // Apply the new velocity with the updated direction
        rb.velocity = new Vector3(xDirection, yDirection, 0) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with PlayerOne and PlayerTwo paddles
        if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo"))
        {
            PlayerBounce(collision.transform);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the Y direction when hitting a wall
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (transform.position.x > 0)
            {
                ResetBall();
                playerOneScore.text = (int.Parse(playerOneScore.text) + 1).ToString();
            }
            else if (transform.position.x < 0)
            {
                ResetBall();
                playerTwoScore.text = (int.Parse(playerTwoScore.text) + 1).ToString();
            }
        }
    }
}
