using UnityEngine;
public class ScoreSignal
{
    public static event System.Action<ScoreData> OnAddScore;

    public static void RaiseScore(ScoreData scoreData)
    {
        OnAddScore?.Invoke(scoreData);
    }

    
}
