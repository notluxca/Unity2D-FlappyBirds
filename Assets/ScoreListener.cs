using System;
using TMPro;
using UnityEngine;

public class ScoreListener : MonoBehaviour
{
    private TextMeshProUGUI _text;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScoreText;
    }

    void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int obj)
    {
        _text.SetText(obj.ToString());
    }
}
