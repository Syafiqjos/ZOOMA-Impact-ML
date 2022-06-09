using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string LevelName;

    [SerializeField] private bool isEnemy;

    [SerializeField] private OrbGenerator orbGenerator;
    [SerializeField] private SplineFollowerManager followerManager;

    private bool isGameOver = false;

    private void Awake()
    {
        LevelName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (CheckGameOver())
        {
            GameOver();
        }
    }

    public bool CheckGameOver()
    {
        if (orbGenerator && orbGenerator.GetForemostOrb())
        {
            return orbGenerator.GetForemostOrb().GetFollower().GetLoopAmount() > 1.0f;
        }
        return false;
    }

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;

            StopAllZumaController();
            StopAllZumaShooter();
            StopAllOrbMovement();
            StartCoroutine(LoadRespectiveScene());
        }
    }

    public void StopAllOrbMovement() {
        SplineFollowerManager[] nes = GameObject.FindObjectsOfType<SplineFollowerManager>();

        foreach (SplineFollowerManager ne in nes) {
            ne.StopMovement();
        }
    }

    public void StopAllZumaShooter() {
        ZumaShooter[] nes = GameObject.FindObjectsOfType<ZumaShooter>();

        foreach (ZumaShooter ne in nes)
        {
            ne.enabled = false;
        }
    }

    public void StopAllZumaController()
    {
        ZumaController[] nes = GameObject.FindObjectsOfType<ZumaController>();

        foreach (ZumaController ne in nes)
        {
            ne.enabled = false;
        }
    }

    IEnumerator LoadRespectiveScene() {
        yield return new WaitForSeconds(5);

        if (isEnemy)
        {
            Debug.Log("Winning");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }

        yield return null;
    }
}
