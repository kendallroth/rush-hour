using Drawing;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : GameSingleton<BoardGenerator>
{
    #region Attributes
    [Header("Spawn Prefabs")]
    [SerializeField]
    [Required]
    private GameObject tilePrefab;
    [SerializeField]
    [Required]
    private GameObject exitPrefab;
    [SerializeField]
    [Required]
    private GameObject carShortPrefab;
    [SerializeField]
    [Required]
    private GameObject carLongPrefab;
    [SerializeField]
    [Required]
    private GameObject carPlayerPrefab;

    [Header("Level")]
    [SerializeField]
    [InfoBox("Generate a level for preview")]
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
    public float TileSize => 1f;
    #endregion

    private Board _board;
    private Board board => _board ? _board : _board = GetComponent<Board>();


    #region Unity Methods
    private void Start()
    {
    }
    #endregion


    #region Custom Methods
    public void GenerateBoard(Level level)
    {
        ClearBoard();

        Debug.Log("Generating game board");

        // Parse and validate vehicle positions
        List<VehicleConfig> vehiclePositions = ParseVehiclePositions(level);
        Debug.Log($"Parsed {vehiclePositions.Count} positions");

        BoardTile[,] tiles = SpawnTiles();
        board.SetTiles(tiles);

        SpawnExit();

        var vehicles = SpawnVehicles(vehiclePositions);
        board.SetVehicles(vehicles);
    }

    private Vehicle[] SpawnVehicles(List<VehicleConfig> vehiclePositions)
    {
        Vehicle[] vehicles = new Vehicle[vehiclePositions.Count];

        for (int idx = 0; idx < vehiclePositions.Count; idx++)
        {
            VehicleConfig position = vehiclePositions[idx];
            GameObject vehiclePrefab = position.Length == 2 ? carShortPrefab : carLongPrefab;
            Quaternion vehicleRotation = position.IdxStride == 1 ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;

            if (position.PlayerVehicle)
            {
                vehiclePrefab = carPlayerPrefab;
            }

            GameObject vehicleObject = Instantiate(vehiclePrefab, Vector3.zero, vehicleRotation, board.VehicleTransform);
            Vehicle vehicle = vehicleObject.GetComponent<Vehicle>();

            vehicle.Init(position);
            vehicle.Snap();

            vehicles[idx] = vehicle;
        }

        return vehicles;
    }

    private void SpawnExit()
    {
        Vector3 exitPosition = new Vector3(Board.SIZE * TileSize - TileSize / 2, 0, -Board.EXIT_ROW_IDX * TileSize);
        Instantiate(exitPrefab, exitPosition, Quaternion.identity, board.TileTransform);
    }

    private BoardTile[,] SpawnTiles()
    {
        BoardTile[,] tiles = new BoardTile[Board.SIZE,Board.SIZE];

        for (int y = 0; y < Board.SIZE; y++)
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                Vector3 position = new Vector3(x * TileSize, 0, -y * TileSize);

                GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, board.TileTransform);
                BoardTile tile = tileObject.GetComponent<BoardTile>();

                tile.Init(new Coordinates(x, y));

                tiles[y, x] = tile;
            }
        }

        return tiles;
    }

    private void ClearBoard()
    {
        Debug.Log("Clearing game board");

        board.ClearTiles();
        board.ClearVehicles();
    }

    /// <summary>
    /// Parse and validate vehicle positions from a level layout string
    /// </summary>
    /// <returns>Level vehicle positions</returns>
    /// <exception cref="Exception">Level validation errors</exception>
    private List<VehicleConfig> ParseVehiclePositions(Level level)
    {
        if (level.LayoutString.Length != Board.SIZE * Board.SIZE)
        {
            throw new Exception($"Invalid level string (size: {level.LayoutString.Length})");
        }

        // Parse all tiles from level string
        Dictionary<string, List<int>> tileMap = new Dictionary<string, List<int>>();
        for (int idx = 0; idx < level.LayoutString.Length; idx++)
        {
            string tileChar = level.LayoutString[idx].ToString();
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
            if (key == "A")
            {
                position.SetPlayer();
            }

            vehiclePositions.Add(position);
        }

        return vehiclePositions;
    }
    #endregion
}
