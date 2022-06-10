using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject unpauseUI;

    void Start()
    {
        Time.timeScale = 1;
        UnpauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 0)
            {
                UnpauseGame();
            }
            else if (Time.timeScale >= 1)
            {
                PauseGame();
            }
        }
    }

    public void RetryGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void UnpauseGame() {
        Time.timeScale = 1;
        RefreshDisplay();
    }

    public void PauseGame() {
        Time.timeScale = 0;
        RefreshDisplay();
    }

    private void RefreshDisplay() {
        if (Time.timeScale == 0)
        {
            pauseUI.SetActive(true);
            unpauseUI.SetActive(false);
        }
        else if (Time.timeScale >= 1) {
            pauseUI.SetActive(false);
            unpauseUI.SetActive(true);
        }
    }
}
