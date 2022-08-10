using com.ootii.Messages;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : GameSingleton<LevelManager>
{
    #region Attributes
    [MinValue(1)]
    [SerializeField]
    private int currentLevelNumber = 1;
    [SerializeField]
    private List<Level> levels = new List<Level>();

    [Button("Generate Current Level")]
    public void GenerateLevelClick()
    {
        if (currentLevelNumber > levels.Count)
        {
            EditorUtility.DisplayDialog("Invalid level", "Cannot generate invalid level number", "Close");
            return;
        }

        LoadCurrentLevel();
    }
    #endregion


    #region Properties
    public int CurrentLevelNumber => currentLevelNumber;

    public bool HasNextLevel => CurrentLevelNumber < levels.Count;
    public bool HasPreviousLevel => CurrentLevelNumber > 1;
    public Level CurrentLevel => levels[CurrentLevelIdx];

    private int CurrentLevelIdx => CurrentLevelNumber - 1;
    #endregion


    #region Unity Methods
    private void Awake()
    {
        // TODO: Eventually load all levels (rather than serialize???)
    }
    #endregion


    #region Custom Methods
    public void LoadCurrentLevel()
    {
        LoadLevel(CurrentLevelNumber);
    }

    public void LoadLevel(int level)
    {
        currentLevelNumber = level;

        BoardGenerator.Instance.GenerateBoard(CurrentLevel);
    }

    public void LoadNextLevel()
    {
        LoadLevel(CurrentLevelNumber + 1);
    }

    public void LoadPreviousLevel()
    {
        LoadLevel(CurrentLevelNumber - 1);
    }
    #endregion
}
