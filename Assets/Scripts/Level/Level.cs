using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelDifficulty
{
    EASY,
    MEDIUM,
    HARD
}

[Serializable]
public class Level
{
    #region Properties
    [ReadOnly]
    public int Number;
    public LevelDifficulty Difficulty;
    /// <summary>
    /// Minimum number of necessary moves to solve
    /// </summary>
    public int Moves;
    /// <summary>
    /// Car layout (string form)
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>A</c></term>
    ///         <description>Player car</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.</c></term>
    ///         <description>Open space</description>
    ///     </item>
    ///     <item>
    ///         <term><c>[B-Z]</c></term>
    ///         <description>Regular cars</description>
    ///     </item>
    /// </list>
    /// <example>
    ///     <code>BB...CD..E.CDAAE.CD..E..F...GGF.HHH.</code>
    /// </example>
    /// </summary>
    public string LayoutString;
    #endregion


    #region Custom Methods
    /// <summary>
    /// Parse and validate vehicle positions from a level layout string
    /// </summary>
    /// <returns>Level vehicle positions</returns>
    /// <exception cref="Exception">Level validation errors</exception>
    public List<VehicleConfig> ParseVehiclePositions()
    {
        return Level.ParseVehiclePositions(LayoutString);
    }

    /// <summary>
    /// Parse and validate vehicle positions from a level layout string
    /// </summary>
    /// <param name="layoutString">Level layout string</param>
    /// <returns>Level vehicle positions</returns>
    /// <exception cref="Exception">Level validation errors</exception>
    public static List<VehicleConfig> ParseVehiclePositions(string layoutString)
    {
        if (layoutString.Length != Board.SIZE * Board.SIZE)
        {
            throw new Exception($"Invalid level string (size: {layoutString.Length})");
        }

        bool hasPlayer = false;

        // Parse all tiles from level string
        Dictionary<string, List<int>> tileMap = new Dictionary<string, List<int>>();
        for (int idx = 0; idx < layoutString.Length; idx++)
        {
            string tileChar = layoutString[idx].ToString();
            if (!tileMap.ContainsKey(tileChar))
            {
                tileMap.Add(tileChar, new List<int>());
            }

            tileMap[tileChar].Add(idx);
        }

        List<VehicleConfig> vehiclePositions = new List<VehicleConfig>();

        // Retrieve vehicle positions from tile map
        foreach (string key in tileMap.Keys)
        {
            // Open spaces are ignored (handled by default tiles)
            if (key == "." || key == "o") continue;
            // Walls are ignored for now (should not exist in levels!)
            if (key == "x") continue;

            List<int> keyPositions = tileMap[key];
            if (keyPositions.Count < 2 || keyPositions.Count > 3)
            {
                throw new Exception($"Invalid vehicle shape (key: {key})");
            }

            // "Stride" indicates the distance between vehicle position indexes (useful for math),
            //   and can be used to determine if vehicle is perfectly horizontal or vertical.
            int idxStride = keyPositions[1] - keyPositions[0];
            if (idxStride != 1 && idxStride != Board.SIZE)
            {
                throw new Exception($"Invalid vehicle shape (key: {key})");
            }
            for (int idx = 2; idx < keyPositions.Count; idx++)
            {
                if (keyPositions[idx] - keyPositions[idx - 1] != idxStride)
                {
                    throw new Exception($"Invalid vehicle shape (key: {key})");
                }
            }

            VehicleConfig position = new VehicleConfig(key, keyPositions[0], keyPositions.Count, idxStride);
            if (key == "X")
            {
                hasPlayer = true;
                position.SetPlayer();
            }

            vehiclePositions.Add(position);
        }

        if (!hasPlayer)
        {
            throw new Exception("No player vehicle found");
        }

        return vehiclePositions;
    }

    /// <summary>
    /// Validate a level string (vehicle positions)
    /// </summary>
    public bool Validate()
    {
        return Level.ValidateLevel(LayoutString);
    }

    /// <summary>
    /// Validate a level string (vehicle positions)
    /// </summary>
    /// <param name="levelString">Level layout string</param>
    public static bool ValidateLevel(string levelString)
    {
        try
        {
            ParseVehiclePositions(levelString);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion
}
