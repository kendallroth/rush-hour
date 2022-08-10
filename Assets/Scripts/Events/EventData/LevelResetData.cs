public class LevelResetData
{
    /// <summary>
    /// Number of moves before reset
    /// </summary>
    public int Moves { get; private set; }

    public LevelResetData(int moves)
    {
        Moves = moves;
    }
}

