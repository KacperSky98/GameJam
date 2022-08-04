using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject RespawnButton;
    [SerializeField] GameObject EndButton;
    [SerializeField] Text CollectedCoins;
    [SerializeField] Text HighScore;
    [SerializeField] Animator animator;
    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        GameOver.SetActive(false);
        RespawnButton.SetActive(false);
        EndButton.SetActive(false);
        animator.enabled = false;
    }
    public void StopTime() {
        CollectedCoins.text = "Collected Coins: " + player.collectedCoins + " / " + player.maxCoins;
        HighScoreData data = HighScoreSystem.LoadHighScore();
        if (data == null)
        {
            HighScore.text = "Personal Best: " + player.collectedCoins + " / " + player.maxCoins;
        }
        else
        {
            HighScore.text = "Personal Best: " + data.collectedCoins + " / " + data.maxCoins;
        }
        Time.timeScale = 0;
    }
    public void BackToMenu() {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
