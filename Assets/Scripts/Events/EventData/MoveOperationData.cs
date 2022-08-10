/// <summary>
/// Movement perform/undo data
/// </summary>
public class MoveOperationData
{
    /// <summary>
    /// Updated move counter (after operation)
    /// </summary>
    public int MoveCounter { get; private set; }
    public GameMove Move { get; private set; }

    public MoveOperationData(GameMove gameMove, int moveCounter)
    {
        Move = gameMove;
        MoveCounter = moveCounter;
    }
}

