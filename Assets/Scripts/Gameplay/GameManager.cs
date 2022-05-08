using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private OrbGenerator orbGenerator;
    [SerializeField] private SplineFollowerManager followerManager;

    private bool isGameOver = false;

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

            Debug.Log("Game Over");
        }
    }
}
