using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseFallingSpeed = -10f;
    private float realFallingSpeed;
    [SerializeField] private float maxFallingSpeed = -50f;
    [SerializeField] private float valueOfIncreasingSpeed = 1.5f;
    [SerializeField] private float movementSpeed = 13f;
    private static float fallingSpeedIncreaseTimer = 1f;
    private float speedIncreaseTimer = fallingSpeedIncreaseTimer;

    private Rigidbody2D rb;
    private float horizontalMove = 0f;

    //Coins
    private int maxCoins = 0;
    public int collectedCoins = 0;
    public int coinsForUpgrade = 5;
    public int currentCoinsForUpgrade;

    //Boost Time
    public bool duringBoost = false;
    private static float boostTime = 3f;
    private float currentBoostTime = boostTime;

    //Bounce
    private float bounceForce = 40f;
    private bool isBounced = false;
    private static float bounceSpeed = 0.5f;
    private float currentBounceSpeed = bounceSpeed;

    //Boost Controllers
    public bool slowActive = false;
    public bool magnetActive = false;
    public bool accelerateActive = false;
    [SerializeField] CoinCatchController coinCatch;

    //Death
    private bool isDead = false;


    void Start()
    {
        currentCoinsForUpgrade = coinsForUpgrade;
        rb = gameObject.GetComponentInParent< Rigidbody2D>();
        rb.gravityScale = 0f;
        maxCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }
    void Update()
    {
        if (!isDead) {
            Falling();
            IncreaseSpeedFalling();
            if (!isBounced)
            {
                Moving();
            }
            BoostController();
            BounceController(); 
        }
    }
    private void Falling() {
        if (accelerateActive)
        {
            realFallingSpeed = baseFallingSpeed * 1.3f;
        }
        else if (slowActive)
        {
            realFallingSpeed = baseFallingSpeed * 0.5f;
        }
        else {
            realFallingSpeed = baseFallingSpeed;
        }
        rb.velocity = new Vector2(rb.velocity.x, realFallingSpeed);
    }
    private void IncreaseSpeedFalling() {
        if (baseFallingSpeed > maxFallingSpeed)
        {
            if (speedIncreaseTimer > 0f)
            {
                speedIncreaseTimer -= Time.deltaTime;
            }
            else
            {
                baseFallingSpeed -= valueOfIncreasingSpeed;
                speedIncreaseTimer = fallingSpeedIncreaseTimer;
            }
        }
        else {
            baseFallingSpeed = maxFallingSpeed;
            Debug.Log(this.transform.position.y);
        }
    }
    private void Moving() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    public void SetBoost() {
        duringBoost = true;
        //Tu dzieje siê magia 
        var number = Random.Range(1, 100);
        if (number <= 33)
        {
            CoinMagnet();
        }
        else if (number > 33 && number <= 66)
        {
            Accelerate();
        }
        else {
            Slow();
        }
    }
    private void CoinMagnet() {
        magnetActive = true;
        coinCatch.GetComponent<CircleCollider2D>().radius = 2f;
    }
    private void Accelerate() {
        accelerateActive = true;
    }
    private void Slow() {
        slowActive = true;
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
                if (accelerateActive)
                {
                    accelerateActive = false;
                }
                else if (slowActive)
                {
                    slowActive = false;
                }
                else {
                    magnetActive = false;
                    coinCatch.GetComponent<CircleCollider2D>().radius = 0.6f;
                }
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
        if (other.CompareTag("Bounce")) {
            if (other.transform.position.x < this.transform.position.x) {
                isBounced = true;
                rb.AddForce(new Vector2(bounceForce,0f), ForceMode2D.Impulse);
            }        
        }
        if (other.CompareTag("Stop")) {
            Debug.Log("GameOver");        
        }
    }


}
