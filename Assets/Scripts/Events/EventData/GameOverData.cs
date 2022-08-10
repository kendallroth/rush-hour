public class GameOverData
{
    public int Moves { get; private set; }
    public float Time { get; private set; }

    public GameOverData(int moves, float time)
    {
        Moves = moves;
        Time = time;
    }
}

