using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VehicleMoveBounds
{
    public bool GridLocked;
    /// <summary>
    /// Possible steps backwards
    /// </summary>
    public int StepsBackward;
    /// <summary>
    /// Possible steps forwards
    /// </summary>
    public int StepsForward;
    /// <summary>
    /// Index of furthest possible position backwards (for vehicle start position)
    /// </summary>
    public int StartPositionIdxMin;
    /// <summary>
    /// Index of furthest possible position forwards (for vehicle start position)
    /// </summary>
    public int StartPositionIdxMax;
    /// <summary>
    /// Coordinates of furthest possible position backwards (for vehicle start position)
    /// </summary>
    public Coordinates StartCoordinatesMin;
    /// <summary>
    /// Coordinates of furthest possible position forwards (for vehicle start position)
    /// </summary>
    public Coordinates StartCoordinatesMax;
    /// <summary>
    /// World position of furthest possible position backwards (for vehicle start position)
    /// </summary>
    public Vector3 StartPositionMin;
    /// <summary>
    /// World position of furthest possible position forwards (for vehicle start position)
    /// </summary>
    public Vector3 StartPositionMax;
    /// <summary>
    /// World center of further possible position backwards (for vehicle center)
    /// </summary>
    public Vector3 CenterPositionMin;
    /// <summary>
    /// World center of further possible position forwards (for vehicle center)
    /// </summary>
    public Vector3 CenterPositionMax;
}

