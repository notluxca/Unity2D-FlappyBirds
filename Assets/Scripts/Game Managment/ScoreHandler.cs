using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    private int score;

    void Awake()
    {
        score = 0;
    }

    private void OnEnable()
    {
        GameEvents.OnScoreAdded += UpdateScore;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreAdded -= UpdateScore;
    }

    private void UpdateScore(int addedValue)
    {
        score += addedValue;
        GameEvents.TriggerScoreChanged(score);
    }
}
