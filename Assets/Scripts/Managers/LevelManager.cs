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

    [TitleGroup("Actions")]
    [PropertySpace(SpaceAfter = 8, SpaceBefore = 0)]
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

    [HorizontalGroup("Actions/Load")]
    [Button("Load All Levels")]
    public void LoadLevelsClick() => LoadAllLevels();

    [HorizontalGroup("Actions/Load")]
    [Button("Validate Levels")]
    public void ValidateLevelsClick()
    {
        var validation = ValidateLevels();
        if (validation.valid)
        {
            EditorUtility.DisplayDialog("Levels validated", "All levels have been validated", "Close");
        }
        else
        {
            string failedLevels = "\n";
            validation.invalidLevels.ForEach((level) =>
            {
                failedLevels += $"\n • Level {level.Number}";
            });
            EditorUtility.DisplayDialog("Levels validation failed", $"Level validation failed for {validation.invalidLevels.Count} levels.{failedLevels}", "Close");
        }
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
        LoadAllLevels();
    }
    #endregion


    #region Custom Methods
    private void LoadAllLevels()
    {
        // TODO: Eventually load levels from textfile (rather than serialize???)
        // TODO: Combine unlocked level stats

        // Assign level numbers
        for (int i = 0; i < levels.Count; i++)
        {
            Level level = levels[i];
            level.Number = i + 1;
        }
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(CurrentLevelNumber);
    }

    public void LoadLevel(int level)
    {
        currentLevelNumber = level;

        Board.Instance.GenerateBoard(CurrentLevel);
    }

    public void LoadNextLevel()
    {
        LoadLevel(CurrentLevelNumber + 1);
    }

    public void LoadPreviousLevel()
    {
        LoadLevel(CurrentLevelNumber - 1);
    }

    public (bool valid, List<Level> invalidLevels) ValidateLevels()
    {
        List<Level> invalidLevels = new List<Level>();

        levels.ForEach((level) =>
        {
            bool valid = level.Validate();
            if (!valid)
            {
                invalidLevels.Add(level);
            }
        });

        return (invalidLevels.Count == 0, invalidLevels);
    }
    #endregion
}
