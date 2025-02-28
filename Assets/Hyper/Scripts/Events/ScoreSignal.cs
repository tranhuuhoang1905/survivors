using UnityEngine;
public class ScoreSignal
{
    public static event System.Action<ScoreEntry> OnAddScore;

    public static void RaiseScore(ScoreEntry scoreEntry)
    {
        OnAddScore?.Invoke(scoreEntry);
    }

    
}
