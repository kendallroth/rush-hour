using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public enum Orientation
{
    HORIZONTAL,
    VERTICAL
}

[Serializable]
public class VehicleConfig
{
    #region Properties
    /// <summary>
    /// Vehicle name/key
    /// </summary>
    public string Key { get; private set; }
    /// <summary>
    /// Vehicle tile position index (board indexes start in top-left)
    /// </summary>
    public int PositionIdx { get; private set; }
    public int Length { get; private set; }
    /// <summary>
    /// Whether vehicle is the player vehicle
    /// </summary>
    public bool PlayerVehicle { get; private set; }
    /// <summary>
    /// Index distance between car segments (helpful for math)
    /// </summary>
    public int IdxStride { get; private set; }
    #endregion

    public VehicleConfig(string key, int positionIdx, int length, int idxStride)
    {
        Key = key;
        PositionIdx = positionIdx;
        Length = length;
        IdxStride = idxStride;
    }

    /// <summary>
    /// Indicate player vehicle
    /// </summary>
    public void SetPlayer()
    {
        PlayerVehicle = true;
    }
}
