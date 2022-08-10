public class LevelCompleteData
{
    public Level Level { get; private set; }
    public int LevelNumber { get; private set; }
    public int Moves { get; private set; }
    //public float Time { get; private set; }

    public LevelCompleteData(Level level, int levelNumber, int moves)
    {
        Level = level;
        LevelNumber = levelNumber;
        Moves = moves;
        //Time = time;
    }
}

