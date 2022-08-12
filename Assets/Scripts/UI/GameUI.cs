using com.ootii.Messages;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    #region Attributes
    [Header("Text")]
    [Required]
    [SerializeField]
    private TextMeshProUGUI levelNumberText;
    [Required]
    [SerializeField]
    private TextMeshProUGUI moveCounterText;
    [SerializeField]
    private Color moveCounterWarning = Color.yellow;
    [Required]
    [SerializeField]
    private TextMeshProUGUI moveMaximumText;

    [Header("Buttons")]
    [Required]
    [SerializeField]
    private Button playButton;
    [Required]
    [SerializeField]
    private Button resetButton;
    [Required]
    [SerializeField]
    private Button undoButton;

    [Header("Lines")]
    [Required]
    [SerializeField]
    private Transform lineImages;
    [SerializeField]
    private Color lineDefaultColor;
    [SerializeField]
    private Color lineSuccessColor;

    [Header("Other")]
    [Required]
    [SerializeField]
    private Image completeIcon;
    #endregion


    #region Properties
    #endregion

    private Image[] borderLineImages;


    #region Unity Methods
    private void Awake()
    {
        levelNumberText.text = "-";
        moveCounterText.text = "0";
        moveMaximumText.text = "/ 00";

        playButton.interactable = false;
        resetButton.interactable = false;
        undoButton.interactable = false;

        playButton.onClick.AddListener(OnPlayClick);
        resetButton.onClick.AddListener(OnResetClick);
        undoButton.onClick.AddListener(OnUndoClick);

        borderLineImages = lineImages.GetComponentsInChildren<Image>();
        completeIcon.gameObject.SetActive(false);

        MessageDispatcher.AddListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.AddListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.AddListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.AddListener(GameEvents.MOVE__PERFORM, OnMoveOperation);
        MessageDispatcher.AddListener(GameEvents.MOVE__UNDO, OnMoveOperation);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayClick);
        resetButton.onClick.RemoveListener(OnResetClick);
        undoButton.onClick.RemoveListener(OnUndoClick);

        MessageDispatcher.RemoveListener(GameEvents.GAME__COMPLETE, OnGameComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__COMPLETE, OnLevelComplete);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__START, OnLevelStart);
        MessageDispatcher.RemoveListener(GameEvents.LEVEL__RESET, OnLevelReset);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__PERFORM, OnMoveOperation);
        MessageDispatcher.RemoveListener(GameEvents.MOVE__UNDO, OnMoveOperation);
    }
    #endregion


    #region Custom Methods
    private void SetBorderColor(Color color)
    {
        borderLineImages.ForEach((line) =>
        {
            line.color = color;
        });
    }
    #endregion


    #region UI Methods
    private void OnPlayClick()
    {
        GameManager.Instance.StartNextLevel();
    }

    private void OnResetClick()
    {
        GameManager.Instance.PerformReset();
    }

    private void OnUndoClick()
    {
        GameManager.Instance.PerformUndo();
    }
    #endregion


    #region Event Handlers
    private void OnGameComplete(IMessage message)
    {
        playButton.gameObject.SetActive(false);

        completeIcon.gameObject.SetActive(true);
    }

    private void OnLevelComplete(IMessage message)
    {
        playButton.interactable = true;
        undoButton.interactable = false;

        SetBorderColor(lineSuccessColor);
    }

    private void OnLevelStart(IMessage message)
    {
        LevelStartData data = (LevelStartData)message.Data;
        levelNumberText.text = $"{data.LevelNumber}";
        moveCounterText.text = "0";
        moveMaximumText.text = $"{data.Level.Moves:D2}";

        playButton.interactable = false;

        moveCounterText.color = Color.white;
        SetBorderColor(lineDefaultColor);
    }

    private void OnLevelReset(IMessage message)
    {
        moveCounterText.text = "0";

        resetButton.interactable = false;
        undoButton.interactable = false;
        playButton.interactable = false;

        playButton.gameObject.SetActive(true);
        completeIcon.gameObject.SetActive(false);

        moveCounterText.color = Color.white;
        SetBorderColor(lineDefaultColor);
    }

    private void OnMoveOperation(IMessage message)
    {
        MoveOperationData data = (MoveOperationData)message.Data;
        moveCounterText.text = $"{data.MoveCounter}";

        resetButton.interactable = data.MoveCounter > 0;
        undoButton.interactable = data.MoveCounter > 0;

        moveCounterText.color = data.MoveCounter > LevelManager.Instance.CurrentLevel.Moves ? moveCounterWarning : Color.white;
    }
    #endregion
}
