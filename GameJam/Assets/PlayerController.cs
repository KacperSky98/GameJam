using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = -10f;
    [SerializeField] private float maxFallingSpeed = -50f;
    [SerializeField] private float valueOfIncreasingSpeed = 1.5f;
    [SerializeField] private float movementSpeed = 13f;
    private static float fallingSpeedIncreaseTimer = 1f;
    private float speedIncreaseTimer = fallingSpeedIncreaseTimer;

    private Rigidbody2D rb;
    private float horizontalMove = 0f;

    private int maxCoins = 0;
    private int collectedCoins = 0;
    private static int coinsForUpgrade = 5;
    private int currentCoinsForUpgrade = coinsForUpgrade;

    private bool duringBoost = false;
    private static float boostTime = 3f;
    private float currentBoostTime = boostTime;

    private float bounceForce = 40f;
    private bool isBounced = false;
    private static float bounceSpeed = 0.5f;
    private float currentBounceSpeed = bounceSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        maxCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    void Update()
    {
        Falling();
        IncreaseSpeedFalling();
        if (!isBounced)
        {
            Moving();
        }
        BoostController();
        BounceController();
    }
    private void Falling() {
        rb.velocity = new Vector2(rb.velocity.x, fallingSpeed);
    }
    private void IncreaseSpeedFalling() {
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
    private void Moving() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    private void SetBoost() {
        duringBoost = true;
        //Tu dzieje siê magia 
    }
    private void BoostController() {
        if (duringBoost) {
            if (currentBoostTime > 0f)
            {
                currentBoostTime -= Time.deltaTime;
            }
            else {
                currentBoostTime = boostTime;
                duringBoost = false;
                Debug.Log("Koniec boosta");
            }
        }    
    }
    private void BounceController() {
        if (isBounced) {
            if (currentBounceSpeed > 0f)
            {
                currentBounceSpeed -= Time.deltaTime;
            }
            else {
                isBounced = false;
                currentBounceSpeed = bounceSpeed;
            }            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Kolizja");
        if (other.CompareTag("Coin")) {
            Destroy(other.gameObject);
            collectedCoins++;
            if (!duringBoost) {
                currentCoinsForUpgrade--;
                if (currentCoinsForUpgrade == 0) {
                    Debug.Log("Dostajesz boosta");
                    SetBoost();
                    currentCoinsForUpgrade = coinsForUpgrade;
                }
            }
        }
        if (other.CompareTag("Bounce")) {
            if (other.transform.position.x < this.transform.position.x) {
                isBounced = true;
                rb.AddForce(new Vector2(bounceForce,0f), ForceMode2D.Impulse);
            }        
        }
        if (other.CompareTag("Stop")) { 
        
        }
    }


}
