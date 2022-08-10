using com.ootii.Messages;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameUI : MonoBehaviour
{
    #region Attributes
    [Required]
    [SerializeField]
    private TextMeshProUGUI levelNumberText;
    [Required]
    [SerializeField]
    private TextMeshProUGUI moveCounterText;
    #endregion


    #region Properties
    #endregion


    #region Unity Methods
    private void Awake()
    {
        levelNumberText.text = "Level: -";
        moveCounterText.text = "Moves: - / -";

        MessageDispatcher.AddListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.AddListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.AddListener(GameEvents.MOVE__PERFORM, OnMoveOperation);
        MessageDispatcher.AddListener(GameEvents.MOVE__UNDO, OnMoveOperation);
    }

    private void OnDestroy()
    {
        
        MessageDispatcher.RemoveListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__PERFORM, OnMoveOperation);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__UNDO, OnMoveOperation);
    }
    #endregion


    #region Custom Methods
    #endregion


    #region Event Handlers
    private void OnGameComplete(IMessage message)
    {
        // TODO
    }

    private void OnLevelComplete(IMessage message)
    {
        // TODO
    }

    private void OnLevelStart(IMessage message)
    {
        LevelStartData data = (LevelStartData)message.Data;
        levelNumberText.text = $"Level: {data.LevelNumber}";
        moveCounterText.text = $"Moves: 0 / {data.Level.Moves}";
    }

    private void OnLevelReset(IMessage message)
    {
        LevelResetData data = (LevelResetData)message.Data;
        moveCounterText.text = $"Moves: 0 / {data.Level.Moves}";
    }

    private void OnMoveOperation(IMessage message)
    {
        int slashIdx = moveCounterText.text.LastIndexOf("/");
        string levelMoves = "-";
        if (slashIdx != -1)
        {
            levelMoves = moveCounterText.text[(slashIdx + 2)..];
        }

        MoveOperationData data = (MoveOperationData)message.Data;
        moveCounterText.text = $"Moves: {data.MoveCounter} / {levelMoves}";
    }
    #endregion
}
