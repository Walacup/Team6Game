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
        // Ensure the ball keeps moving with clamped velocity
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        // Start the ball moving along the Z-axis, with a random direction on the X-axis
        float randomDirection = Random.value > 0.5f ? 1f : -1f;
        rb.velocity = new Vector3(Random.Range(-0.5f, 0.5f), 0, randomDirection) * (initialSpeed + speedIncrease * hitCounter);
    }

   private void ResetBall()
 {
    // Reset ball position and velocity after a goal
    rb.velocity = Vector3.zero;
    transform.position = new Vector3(0.16f, 0.982f, 0f);  // Move the ball to the specified coordinates
    hitCounter = 0;  // Reset hit counter
    StartBall();  // Restart ball movement
 }
    private void PlayerBounce(Transform paddle)
    {
        hitCounter++;

        Vector3 ballPos = transform.position;
        Vector3 paddlePos = paddle.position;

        float zDirection = transform.position.z > 0 ? -1 : 1;  // Move the ball along the Z-axis
        float xDirection = (ballPos.x - paddlePos.x) / paddle.GetComponent<Collider>().bounds.size.x;  // Calculate X direction based on where the ball hits the paddle

        if (xDirection == 0)
        {
            xDirection = 0.25f;  // Ensure the ball doesn't move perfectly straight
        }

        // Apply the new velocity with the updated direction
        rb.velocity = new Vector3(xDirection, 0, zDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with PlayerOne and PlayerTwo paddles
        if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo"))
        {
            PlayerBounce(collision.transform);  // Bounce off the player's paddle
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Reverse the X direction when hitting a wall, while keeping the Z-axis velocity the same
            rb.velocity = new Vector3(-rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Handle goals (ball passing behind a paddle)
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (transform.position.z > 0)
            {
                ResetBall();
                playerOneScore.text = (int.Parse(playerOneScore.text) + 1).ToString();
            }
            else if (transform.position.z < 0)
            {
                ResetBall();
                playerTwoScore.text = (int.Parse(playerTwoScore.text) + 1).ToString();
            }
        }
    }
}
