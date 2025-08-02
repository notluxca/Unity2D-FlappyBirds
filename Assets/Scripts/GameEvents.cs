using System;
using UnityEngine;

public static class GameEvents
{
    public static Action OnGameStarted;

    public static Action<int> OnScoreAdded;
    public static Action<int> OnScoreChanged;

    public static void TriggerGameStarted() { OnGameStarted?.Invoke(); }
    public static void TriggerScoreChanged(int newScore) { OnScoreChanged?.Invoke(newScore); }
    public static void TriggerScoreAdded(int scoreToAdd) { OnScoreAdded?.Invoke(scoreToAdd); }
}
