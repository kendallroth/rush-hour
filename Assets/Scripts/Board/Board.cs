using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileGenerator))]
[RequireComponent(typeof(VehicleGenerator))]
public class Board : GameSingleton<Board>
{
    public const int SIZE = 6;
    public const int EXIT_ROW_IDX = 2;

    #region Attributes
    [Header("Spawn Parents")]
    [SerializeField]
    [Required]
    private Transform tileTransform;
    [SerializeField]
    [Required]
    private Transform vehicleTransform;
    [SerializeField]
    [Required]
    private Transform exitTransform;

    [Header("Level")]
    [InfoBox("Generate a level for preview", InfoMessageType.Warning)]
    [InfoBox("AA...OP..Q.OPXXQ.OP..Q..B...CCB.RRR.", InfoMessageType.None)]
    [SerializeField]
    private Level debugLevel;

    [TitleGroup("Actions")]
    [HorizontalGroup("Actions/Actions")]
    [Button("Generate (Debug)")]
    private void GenerateClick() => GenerateBoard(debugLevel);
    [HorizontalGroup("Actions/Actions")]
    [Button("Clear")]
    private void ClearClick() => ClearBoard();
    #endregion


    #region Properties
    public BoardTile[,] Tiles { get; private set; }
    public Vehicle[] Vehicles { get; private set; }

    public Transform ExitTransform => exitTransform;
    public Transform TileTransform => tileTransform;
    public Transform VehicleTransform => vehicleTransform;

    public static int EXIT_START_POSITION => (EXIT_ROW_IDX * SIZE) + (SIZE - 2);
    public float TileSize => 1f;
    #endregion


    #region Unity Methods
    #endregion


    #region Custom Methods
    public void ClearBoard()
    {
        Debug.Log("Clearing game board");

        ClearTiles();
        ClearVehicles();
    }

    private void ClearTiles()
    {
        exitTransform.RemoveChildren();
        tileTransform.RemoveChildren();
        Tiles = new BoardTile[,] { };
    }

    private void ClearVehicles()
    {
        vehicleTransform.RemoveChildren();
        Vehicles = new Vehicle[] { };
    }

    public void GenerateBoard(Level level)
    {
        ClearBoard();

        TileGenerator tileGenerator = GetComponent<TileGenerator>();
        VehicleGenerator vehicleGenerator = GetComponent<VehicleGenerator>();

        List<VehicleConfig> vehiclePositions = level.ParseVehiclePositions();
        Debug.Log($"Parsed {vehiclePositions.Count} positions");

        Tiles = tileGenerator.SpawnTiles();

        Vehicles = vehicleGenerator.SpawnVehicles(vehiclePositions);
    }

    /// <summary>
    /// Check whether a board cell index is occupied
    /// </summary>
    public bool CheckOccupied(int index)
    {
        return GetVehicleAt(index) != null;
    }

    /// <summary>
    /// Get vehicle movement step boundaries
    /// </summary>
    public (int stepsForward, int stepsBackward) GetMoveBoundsAsSteps(Vehicle vehicle)
    {
        int offset = vehicle.Orientation == Orientation.HORIZONTAL
            ? (int)Math.Floor(vehicle.PositionIdxStart % SIZE / 1f)
            : (int)Math.Floor(vehicle.PositionIdxStart / SIZE / 1f);

        int maxStepsBackward = -offset;
        int maxStepsForward = SIZE - vehicle.Length - offset;

        int stepsBackward = 0;
        int stepsForward = 0;
        for (int steps = -1; steps >= maxStepsBackward; steps--)
        {
            if (CheckOccupied(vehicle.PositionIdxStart - vehicle.IdxStride * -steps))
                break;

            stepsBackward = steps;
        }
        for (int steps = 1; steps <= maxStepsForward; steps++)
        {
            if (CheckOccupied(vehicle.PositionIdxEnd + vehicle.IdxStride * steps))
                break;

            stepsForward = steps;
        }

        return (stepsForward, stepsBackward);
    }

    /// <summary>
    /// Get vehicle movement boundary stats
    /// </summary>
    public VehicleMoveBounds GetMoveBounds(Vehicle vehicle)
    {
        var (stepsForward, stepsBackward) = GetMoveBoundsAsSteps(vehicle);

        int forwardStartIdx = vehicle.PositionIdxStart - -stepsBackward * vehicle.IdxStride;
        Coordinates forwardStartCoordinates = Coordinates.FromPositionIndex(forwardStartIdx);
        Vector3 forwardStartPosition = forwardStartCoordinates.GetWorldPosition(TileSize);

        int backwardEndIdx = vehicle.PositionIdxEnd + stepsForward * vehicle.IdxStride;
        Coordinates backwardEndCoordinates = Coordinates.FromPositionIndex(backwardEndIdx);
        Vector3 backwardEndPosition = backwardEndCoordinates.GetWorldPosition(TileSize);

        Vector3 forwardEndCenter = Vector3Extensions.PointAlongLine(forwardStartPosition, backwardEndPosition, TileSize * vehicle.Length / 2 - TileSize / 2);
        Vector3 backwardStartCenter = Vector3Extensions.PointAlongLine(forwardStartPosition, backwardEndPosition, TileSize * vehicle.Length / 2 - TileSize / 2, true);

        return new VehicleMoveBounds
        {
            GridLocked = forwardStartIdx == vehicle.PositionIdxStart && backwardEndIdx == vehicle.PositionIdxEnd,
            StepsBackward = stepsBackward,
            StepsForward = stepsForward,
            StartPositionIdxMax = forwardStartIdx,
            StartPositionIdxMin = backwardEndIdx,
            StartCoordinatesMax = forwardStartCoordinates,
            StartCoordinatesMin = backwardEndCoordinates,
            StartPositionMax = forwardStartPosition,
            StartPositionMin = backwardEndPosition,
            CenterPositionMax = forwardEndCenter,
            CenterPositionMin = backwardStartCenter,
        };
    }

    /// <summary>
    /// Get vehicle at a board cell
    /// </summary>
    /// <param name="index">Board cell index</param>
    public Vehicle? GetVehicleAt(int index)
    {
        foreach (Vehicle vehicle in Vehicles)
        {
            // Check whether any tiles in the vehicle overlap with the selected index (utilizing stride)
            int tileIdx = vehicle.PositionIdxStart;
            for (int j = 0; j < vehicle.Length; j++)
            {
                if (tileIdx == index) return vehicle;
                tileIdx += vehicle.IdxStride;
            }
        }

        return null;
    }
    #endregion
}
