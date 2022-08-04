using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject RespawnButton;
    [SerializeField] GameObject EndButton;
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        GameOver.SetActive(false);
        RespawnButton.SetActive(false);
        EndButton.SetActive(false);
        animator.enabled = false;
    }
    public void StopTime() {
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
