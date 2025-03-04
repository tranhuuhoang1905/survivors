using UnityEngine;
public class ScoreEvent
{
    public static event System.Action<ScoreEntry> OnAddScore;
    public static event System.Action<int, int, int> OnExpUpdated;
    public static event System.Action<int> OnScoreUpdated;
    public static event System.Action<int> OnTimeUpdated;

    public static void RaiseScore(ScoreEntry scoreEntry)
    {
        OnAddScore?.Invoke(scoreEntry);
    }

    public static void RaiseScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    public static void RaiseTimeUpdated(int newScore)
    {
        OnTimeUpdated?.Invoke(newScore);
    }

    public static void RaiseExpUpdated(int newScore,int maxScore,int newLevel)
    {
        OnExpUpdated?.Invoke(newScore,maxScore,newLevel);
    }
    
}
