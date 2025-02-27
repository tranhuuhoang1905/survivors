using UnityEngine;
public class ScoreSignal
{
    public static event System.Action<int> OnAddScore;
    public static event System.Action<int> OnScoreUpdated;

    public static void RaiseScore(int scoreAmount)
    {
        OnAddScore?.Invoke(scoreAmount);
    }

    public static void RaiseScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }
}
