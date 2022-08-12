using com.ootii.Messages;
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
    public Level CurrentLevel => LevelManager.Instance.CurrentLevel;
    [ShowInInspector]
    public int LevelNumber => LevelManager.Instance.CurrentLevelNumber;
    [ShowInInspector]
    public int MoveCount => gameMoves.Count;
    public bool LevelComplete { get; private set; } = false;

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

    private void Start()
    {
        LevelComplete = false;
        LevelManager.Instance.LoadCurrentLevel();
        LevelStart();
    }

    private void OnEnable()
    {
        playerInput.Debug.Enable();
        playerInput.Debug.NextLevel.performed += NextLevelHandler;

        playerInput.Keys.Enable();
        playerInput.Keys.Undo.performed += UndoLastMoveHandler;
        playerInput.Keys.Reset.performed += ResetHandler;
    }

    private void OnDisable()
    {
        playerInput.Debug.Disable();
        playerInput.Debug.NextLevel.performed -= NextLevelHandler;

        playerInput.Keys.Disable();
        playerInput.Keys.Undo.performed -= UndoLastMoveHandler;
        playerInput.Keys.Reset.performed -= ResetHandler;
    }

    // "Temporary" functions to void subscribing with lambda function
    private void UndoLastMoveHandler(InputAction.CallbackContext ctx) => PerformUndo();
    private void ResetHandler(InputAction.CallbackContext ctx) => PerformReset();
    private void NextLevelHandler(InputAction.CallbackContext ctx)
    {
        LevelFinish();
        StartNextLevel();
    }
    #endregion


    #region Custom Methods
    private void GameFinish()
    {
        MessageDispatcher.SendMessage(GameEvents.GAME__COMPLETE);
    }

    private void LevelStart()
    {
        LevelComplete = false;
        gameMoves.Clear();

        LevelStartData levelStartData = new LevelStartData(CurrentLevel, LevelNumber);
        MessageDispatcher.SendMessageData(GameEvents.LEVEL__START, levelStartData, -1);
    }

    private void LevelFinish()
    {
        LevelComplete = true;

        // NOTE: Cannot clear move stack as player can still reset level (currently requires undoing moves by count)

        LevelCompleteData levelCompleteData = new LevelCompleteData(CurrentLevel, LevelNumber, MoveCount);
        MessageDispatcher.SendMessageData(GameEvents.LEVEL__COMPLETE, levelCompleteData);

        if (!LevelManager.Instance.HasNextLevel)
        {
            GameFinish();
        }
    }

    public void StartNextLevel()
    {
        if (!LevelComplete) return;

        LevelManager.Instance.LoadNextLevel();
        LevelStart();
    }

    public void PerformMove(Vehicle vehicle, int steps)
    {
        if (LevelComplete) return;

        // TODO: Potentially validate move (already technically handled by bounds)?

        vehicle.MoveByStep(steps);

        GameMove move = new GameMove(vehicle, steps);
        gameMoves.Push(move);

        MoveOperationData movePerformData = new MoveOperationData(move, MoveCount);
        MessageDispatcher.SendMessageData(GameEvents.MOVE__PERFORM, movePerformData);

        if (vehicle.PlayerVehicle && vehicle.PositionIdxStart == Board.EXIT_START_POSITION)
        {
            LevelFinish();
        }
    }

    public void PerformUndo()
    {
        if (LevelComplete || MoveCount <= 0) return;

        // Retrieve last move before undoing to be able to access after
        GameMove move = gameMoves.Peek();

        UndoMoves(1);

        MoveOperationData moveUndoData = new MoveOperationData(move, MoveCount);
        MessageDispatcher.SendMessageData(GameEvents.MOVE__UNDO, moveUndoData);
    }

    public void PerformReset()
    {
        // Allow resetting level even after completion
        if (MoveCount <= 0) return;
        LevelComplete = false;

        int previousMoveCount = MoveCount;
        UndoMoves(MoveCount);

        LevelResetData levelResetData = new LevelResetData(CurrentLevel, LevelNumber, previousMoveCount);
        MessageDispatcher.SendMessageData(GameEvents.LEVEL__RESET, levelResetData);
    }

    private void UndoMoves(int moves = 1)
    {
        if (LevelComplete || MoveCount <= 0) return;

        int maxMoves = MoveCount;

        for (int i = 0; i < moves && i < maxMoves; i++)
        {
            GameMove move = gameMoves.Pop();
            move.Vehicle.MoveByStep(-move.Steps);
        }
    }
    #endregion
}
