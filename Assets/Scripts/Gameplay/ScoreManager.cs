using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float scorePointAdderDefault = 1000;
    [SerializeField] private float scoreComboMultiplier = 1.5f;

    public delegate void OnComboEventHandler(int combo);
    public event OnComboEventHandler OnCombo;

    public delegate void OnScoreAddedEventHandler(float scoreTotal, float scoreAccumulate);
    public event OnScoreAddedEventHandler OnScoreAdded;

    private float scoreTotal = 0;

    public void AddScore(float score, int combo = 1) {
        float bonusScore = (combo - 1) * scoreComboMultiplier * score;
        float scoreAcc = score + bonusScore;

        scoreTotal += scoreAcc;

        if (combo > 1) {
            OnCombo?.Invoke(combo);
        }
        OnScoreAdded?.Invoke(scoreTotal, scoreAcc);
    }

    public void ExecuteScore(int combo = 1)
    {
        AddScore(scorePointAdderDefault, combo);
    }
}
