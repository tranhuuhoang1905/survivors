[System.Serializable]
public class ScoreData
{
    public int scoreType; // 1, 2, 3
    public int value;

    public ScoreData(int type, int val)
    {
        scoreType = type;
        value = val;
    }
}