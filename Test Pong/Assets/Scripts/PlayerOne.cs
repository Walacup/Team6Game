using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveInput = 0f;

      
        if (Input.GetKey(KeyCode.W))
        {
            moveInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveInput = -1f;
        }

        
        float moveAmount = moveInput * speed * Time.deltaTime;

        
        transform.Translate(moveAmount, 0f, 0f);

       
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -3.8f, 3.8f); 
        transform.position = clampedPosition;
    }
}
