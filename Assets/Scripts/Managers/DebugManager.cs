using com.ootii.Messages;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : GameSingleton<DebugManager>
{
    #region Attributes
    [Header("Tiles")]
    [OdinSerialize]
    public bool DrawTileBorders = true;
    [OdinSerialize]
    public bool DrawTileCoordinates = true;

    [Header("Vehicles")]
    [OdinSerialize]
    public bool DrawVehicleBounds = true;

    [Header("Logs")]
    [OdinSerialize]
    public bool LogEvents = true;
    #endregion


    #region Properties
    #endregion


    #region Unity Methods
    private void Awake()
    {
        MessageDispatcher.AddListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.AddListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.AddListener(GameEvents.MOVE__PERFORM, OnMovePerform);
        MessageDispatcher.AddListener(GameEvents.MOVE__UNDO, OnMoveUndo);
    }

    private void OnDestroy()
    {
        MessageDispatcher.RemoveListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__PERFORM, OnMovePerform);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__UNDO, OnMoveUndo);
    }
    #endregion


    #region Custom Methods
    #endregion


    #region Event Handlers
    private void OnGameComplete(IMessage message)
    {
        if (!LogEvents) return;
        Debug.Log("Player has completed the game!!!");
    }

    private void OnLevelComplete(IMessage message)
    {
        if (!LogEvents) return;
        LevelCompleteData data = (LevelCompleteData)message.Data;
        Debug.Log($"Player has completed level {data.LevelNumber} ({data.Moves} moves)!");
    }

    private void OnLevelStart(IMessage message)
    {
        if (!LogEvents) return;
        LevelStartData data = (LevelStartData)message.Data;
        Debug.Log($"Player has started level {data.LevelNumber}");
    }

    private void OnLevelReset(IMessage message)
    {
        if (!LogEvents) return;
        LevelResetData data = (LevelResetData)message.Data;
        Debug.Log($"Reset game ({data.Moves} moves)");
    }

    private void OnMovePerform(IMessage message)
    {
        if (!LogEvents) return;
        MoveOperationData data = (MoveOperationData)message.Data;
        Debug.Log($"Moved {data.Move.Vehicle.Key} by {data.Move.Steps} steps (move {data.MoveCounter})");
    }

    private void OnMoveUndo(IMessage message)
    {
        if (!LogEvents) return;
        MoveOperationData data = (MoveOperationData)message.Data;
        Debug.Log($"Undid previous move for {data.Move.Vehicle.Key} by {-data.Move.Steps} steps");
    }
    #endregion
}
