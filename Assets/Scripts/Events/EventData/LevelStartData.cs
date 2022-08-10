public class LevelStartData
{
    public Level Level { get; private set; }
    public int LevelNumber { get; private set; }

    public LevelStartData(Level level, int levelNumber)
    {
        Level = level;
        LevelNumber = levelNumber;
    }
}

