using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = -10;
    [SerializeField] private float maxFallingSpeed = -55;
    [SerializeField] private float valueOfIncreasingSpeed = 0.1f;
    [SerializeField] private float movementSpeed = 10f;
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    void Update()
    {
        Falling();
        IncreaseSpeedFalling();
        Moving();
        BoostController();
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
    }


}
