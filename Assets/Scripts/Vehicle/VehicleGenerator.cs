using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VehicleColorDictionary : UnitySerializedDictionary<string, Color> { }

public class VehicleGenerator : MonoBehaviour
{
    #region Attributes
    [Header("Spawn Prefabs")]
    [SerializeField]
    [Required]
    private GameObject carShortPrefab;
    [SerializeField]
    [Required]
    private GameObject carLongPrefab;
    [SerializeField]
    [Required]
    private GameObject carPlayerPrefab;

    [Header("Colors")]
    [SerializeField]
    private VehicleColorDictionary vehicleColors = new VehicleColorDictionary();
    #endregion


    #region Properties
    public Dictionary<string, Color> VehicleColors => vehicleColors;
    #endregion

    private Board _board;
    private Board board => _board ? _board : _board = GetComponent<Board>();


    #region Unity Methods
    private void Awake()
    {
        // TODO: Load vehicles colours from file
    }
    #endregion


    #region Custom Methods
    public Vehicle[] SpawnVehicles(List<VehicleConfig> vehiclePositions)
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

            Color vehicleColor = vehicleColors.GetValueOrDefault(position.Key, Color.gray);
            vehicle.Init(position, vehicleColor);
            vehicle.Snap();

            vehicles[idx] = vehicle;
        }

        return vehicles;
    }
    #endregion
}
