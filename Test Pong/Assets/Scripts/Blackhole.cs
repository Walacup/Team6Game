<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject ball;            // The ball object
    public float blackHoleForce = 20f; // Force to apply on the ball
    public float spawnInterval = 5f;   // Time interval to spawn black holes
    public float blackHoleDuration = 3f; // Time the black hole stays active
    public Vector3 spawnArea;          // The area in which the black hole can spawn

    void Start()
    {
        // Start the black hole spawning process
        StartCoroutine(SpawnBlackHole());
    }

    IEnumerator SpawnBlackHole()
    {
        while (true)
        {
            // Wait for the spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the black hole at a random position within the spawn area
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            GameObject newBlackHole = Instantiate(gameObject, randomPosition, Quaternion.identity);

            // Destroy the black hole after a set duration
            Destroy(newBlackHole, blackHoleDuration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Apply a forward force to the ball when it enters the black hole's trigger
                Vector3 forceDirection = ball.transform.forward * blackHoleForce;
                ballRigidbody.AddForce(forceDirection, ForceMode.Impulse);
            }
        }
    }
}
=======
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public GameObject ball;               // Reference to the ball object
    public float bounceForce = 20f;       // Force to bounce the ball away with high speed

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ball)
        {
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Get the direction from the ball to the blackhole
                Vector3 directionFromBall = transform.position - ball.transform.position;

                // Normalize the direction vector and negate it to point away from the blackhole
                directionFromBall.Normalize();

                // Apply a force in the opposite direction of the collision
                ballRigidbody.AddForce(-directionFromBall * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
>>>>>>> Stashed changes
