public class LevelResetData
{
    public Level Level { get; private set; }
    public int LevelNumber { get; private set; }
    /// <summary>
    /// Number of moves before reset
    /// </summary>
    public int Moves { get; private set; }

    public LevelResetData(Level level, int levelNumber, int moves)
    {
        Level = level;
        LevelNumber = levelNumber;
        Moves = moves;
    }
}

