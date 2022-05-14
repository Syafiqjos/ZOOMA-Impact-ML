using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI displayText;

    private void Start()
    {
        scoreManager.OnScoreAdded += UpdateDisplay;

        UpdateDisplay(0, 0);
    }

    private void UpdateDisplay(float scoreTotal, float scoreAcc)
    {
        displayText.text = "Score:" + scoreTotal.ToString("0000000");
    }
}
