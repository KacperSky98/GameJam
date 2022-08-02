using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeRockAI : MonoBehaviour
{
    [SerializeField] private float FallingSpeed = -5f;
    [SerializeField] private float maxFallingSpeed = -50f;
    [SerializeField] private float valueOfIncreasingSpeed = 1.5f;
    private static float fallingSpeedIncreaseTimer = 1f;
    private float speedIncreaseTimer = fallingSpeedIncreaseTimer;

    [SerializeField] private Rigidbody2D rb;
    void Start()
    {
        
    }
    void Update()
    {
        Falling();
        IncreaseSpeedFalling();
    }
    private void Falling()
    {
        rb.velocity = new Vector2(rb.velocity.x, FallingSpeed);
    }
    private void IncreaseSpeedFalling()
    {
        if (FallingSpeed > maxFallingSpeed)
        {
            if (speedIncreaseTimer > 0f)
            {
                speedIncreaseTimer -= Time.deltaTime;
            }
            else
            {
                FallingSpeed -= valueOfIncreasingSpeed;
                speedIncreaseTimer = fallingSpeedIncreaseTimer;
            }
        }
        else
        {
            FallingSpeed = maxFallingSpeed;
            Debug.Log(this.transform.position.y);
        }
    }
}
