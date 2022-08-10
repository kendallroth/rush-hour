using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct GameMove
{
    public Vehicle Vehicle;
    public int Steps;

    public GameMove(Vehicle vehicle, int steps)
    {
        Vehicle = vehicle;
        Steps = steps;
    }
}


public class GameManager : GameSingleton<GameManager>
{
    #region Attributes
    [TitleGroup("Actions")]
    [HorizontalGroup("Actions/Actions")]
    [Button("Undo")]
    private void UndoClick() => PerformUndo();
    [HorizontalGroup("Actions/Actions")]
    [Button("Reset Game")]
    private void ResetClick() => PerformReset();
    #endregion


    #region Properties
    [ShowInInspector]
    public int MoveCount => gameMoves.Count;
    /// <summary>
    /// Whether movement is locked (after winning, etc)
    /// </summary>
    [ReadOnly]
    [ShowInInspector]
    public bool MovementLocked { get; private set; } = false;

    public Vehicle[] Vehicles => Board.Instance.Vehicles;
    #endregion

    private PlayerInput playerInput;
    private Stack<GameMove> gameMoves = new Stack<GameMove>();


    #region Unity Methods
    private void Awake()
    {
        gameMoves = new Stack<GameMove>();
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Keys.Enable();
        playerInput.Keys.Undo.performed += UndoLastMoveHandler;
        playerInput.Keys.Reset.performed += ResetHandler;
    }

    private void OnDisable()
    {
        playerInput.Keys.Disable();
        playerInput.Keys.Undo.performed -= UndoLastMoveHandler;
        playerInput.Keys.Reset.performed -= ResetHandler;
    }

    // "Temporary" functions to void subscribing with lambda function
    private void UndoLastMoveHandler(InputAction.CallbackContext ctx) => PerformUndo();
    private void ResetHandler(InputAction.CallbackContext ctx) => PerformReset();
    #endregion


    #region Custom Methods
    public void LevelStart()
    {
        MovementLocked = false;

        // TODO
    }

    public void LevelFinish()
    {
        MovementLocked = true;

        Debug.Log("Player has won!");

        // TODO
    }

    public void PerformMove(Vehicle vehicle, int steps)
    {
        if (MovementLocked) return;

        // TODO: Potentially validate move (already technically handled by bounds)?

        vehicle.MoveByStep(steps);

        gameMoves.Push(new GameMove(vehicle, steps));

        Debug.Log($"Moved {vehicle.Key} by {steps} steps (move {MoveCount})");

        Debug.Log($"{vehicle.PositionIdxStart} {Board.EXIT_START_POSITION}");
        if (vehicle.PlayerVehicle && vehicle.PositionIdxStart == Board.EXIT_START_POSITION)
        {
            LevelFinish();
        }
    }

    public void PerformUndo()
    {
        if (MovementLocked || MoveCount <= 0) return;

        // Retrieve last move before undoing to be able to access after
        GameMove move = gameMoves.Peek();

        UndoMoves(1);

        Debug.Log($"Undid previous move for {move.Vehicle.Key} by {-move.Steps} steps");
    }

    public void PerformReset()
    {
        if (MovementLocked || MoveCount <= 0) return;

        Debug.Log($"Reset game ({MoveCount} moves)");

        UndoMoves(MoveCount);
    }

    private void UndoMoves(int moves = 1)
    {
        if (MovementLocked || MoveCount <= 0) return;

        int maxMoves = MoveCount;

        for (int i = 0; i < moves && i < maxMoves; i++)
        {
            GameMove move = gameMoves.Pop();
            move.Vehicle.MoveByStep(-move.Steps);
        }
    }
    #endregion
}
