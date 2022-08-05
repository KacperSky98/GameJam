using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int maxCoins = 0;
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
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Text deathAndVictoryScreen;

    //Animator
    [SerializeField] Animator animator;

    //Slow Terrain
    private bool onSlowTerrain = false;
    [SerializeField] private float slowValue = 0.3f;

    void Start()
    {
        Time.timeScale = 1;
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
            realFallingSpeed = baseFallingSpeed * 1.2f;
        }
        else if (slowActive)
        {
            realFallingSpeed = baseFallingSpeed * 0.5f;
        }
        else {
            realFallingSpeed = baseFallingSpeed;
        }
        if (onSlowTerrain) {
            realFallingSpeed *= slowValue;
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
        var number = Random.Range(1, 100);
        if (number <= 15)
        {
            Slow();
        }
        else if (number > 15 && number <= 56)
        {
            Accelerate();
        }
        else {
            CoinMagnet();
        }
    }
    private void CoinMagnet() {
        if (!magnetActive)
        {
            magnetActive = true;
            coinCatch.GetComponent<CircleCollider2D>().radius = 0.8f;
        }
        else {
            magnetActive = false;
            coinCatch.GetComponent<CircleCollider2D>().radius = 0.3f;
        }
        animator.SetTrigger("Magnet");
    }
    private void Accelerate() {
        if (!accelerateActive)
        {
            accelerateActive = true;
        }
        else {
            accelerateActive = false;
        }
        animator.SetTrigger("Fast");
    }
    private void Slow() {
        if (!slowActive)
        {
            slowActive = true;
        }
        else {
            slowActive = false;
        }
        animator.SetTrigger("Slow");
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
                    Accelerate();
                }
                else if (slowActive)
                {
                    Slow();
                }
                else {
                    CoinMagnet();
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
    public void SaveHighScore() {
        HighScoreData data = HighScoreSystem.LoadHighScore();
        if (data == null)
        {
            HighScoreSystem.SaveHighScore(this);
        }
        else {
            if (data.collectedCoins < collectedCoins) {
                HighScoreSystem.SaveHighScore(this);
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
            SaveHighScore();
            deathAndVictoryScreen.text = "Game Over";
            isDead = true;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            rb.velocity = new Vector3(0f, 0f, 0f);
            deathScreen.SetActive(true);
            deathScreen.GetComponent<Animator>().enabled = true;
            Debug.Log("GameOver");        
        }
        if (other.CompareTag("Slow")) {
            onSlowTerrain = true;    
        }
        if (other.CompareTag("Win")) {
            SaveHighScore();
            deathAndVictoryScreen.text = "\"Victory\"";
            isDead = true;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            rb.velocity = new Vector3(0f, 0f, 0f);
            deathScreen.SetActive(true);
            deathScreen.GetComponent<Animator>().enabled = true;
            Debug.Log("Win");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Slow"))
        {
            onSlowTerrain = false;
        }
    }


}
