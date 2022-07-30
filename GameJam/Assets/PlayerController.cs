using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = -10;
    [SerializeField] private float maxFallingSpeed = -55;
    [SerializeField] private float valueOfIncreasingSpeed = 0.1f;
    private static float fallingSpeedIncreaseTimer = 1f;
    private float speedIncreaseTimer = fallingSpeedIncreaseTimer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }
    void Update()
    {
        Falling();
        IncreaseSpeedFalling();
    }
    private void Falling() {
        rb.velocity = new Vector2(rb.velocity.x, fallingSpeed);  
    }
    private void IncreaseSpeedFalling (){
        if (fallingSpeed > maxFallingSpeed)
        {
            if (speedIncreaseTimer > 0f)
            {
                speedIncreaseTimer -= Time.deltaTime;
            }
            else
            {
                fallingSpeed -= valueOfIncreasingSpeed;
                speedIncreaseTimer = fallingSpeedIncreaseTimer;
            }
        }
        else {
            fallingSpeed = maxFallingSpeed;
            Debug.Log(this.transform.position.y);
        }
    }
}
