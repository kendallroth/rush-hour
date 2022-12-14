using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    #region Attributes
    [ShowInInspector]
    [ReadOnly]
    public string Key { get; private set; }
    #endregion


    #region Properties
    public int PositionIdxStart { get; private set; }
    public int PositionIdxEnd => PositionIdxStart + IdxStride * (Length - 1);

    public Coordinates CoordinatesStart => Coordinates.FromPositionIndex(PositionIdxStart);
    public Coordinates CoordinatesEnd => Coordinates.FromPositionIndex(PositionIdxEnd);

    public int Length { get; private set; }
    /// <summary>
    /// Index distance between car segments (helpful for math)
    /// </summary>
    public int IdxStride { get; private set; }

    public bool PlayerVehicle { get; private set; }
    public Orientation Orientation => IdxStride == 1 ? Orientation.HORIZONTAL : Orientation.VERTICAL;
    #endregion

    private Transform vehicleTransform;
    private Vector3 restingPosition;


    #region Unity Methods
    private void Awake()
    {
        vehicleTransform = transform.GetChild(0);
        restingPosition = vehicleTransform.localPosition;
    }
    #endregion


    #region Custom Methods
    public void Init(VehicleConfig position, Color color)
    {
        Key = position.Key;
        IdxStride = position.IdxStride;
        PositionIdxStart = position.PositionIdx;
        PlayerVehicle = position.PlayerVehicle;
        Length = position.Length;

        // NOTE: No need to cache since "Init" is only called once per level
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        Renderer renderer = GetComponentInChildren<Renderer>();

        renderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_BaseColor", color);
        renderer.SetPropertyBlock(propertyBlock);
    }

    /// <summary>
    /// Get world position of the vehicle's center
    /// </summary>
    public Vector3 GetCenterWorldPosition()
    {
        float tileSize = Board.Instance.TileSize;
        Vector3 start = CoordinatesStart.GetWorldPosition(tileSize);
        Vector3 end = CoordinatesEnd.GetWorldPosition(tileSize);

        return (start + end) * 0.5f;
    }

    /// <summary>
    /// Move vehicle by number of steps
    /// </summary>
    /// <param name="steps">Number of steps to move vehicle</param>
    /// <param name="snap">Whether to snap vehicle to required position after moving</param>
    public void MoveByStep(int steps, bool snap = true)
    {
        int startIdx = PositionIdxStart + steps * IdxStride;
        MoveToIndex(startIdx, snap);
    }

    /// <summary>
    /// Move the vehicle to a new start index
    /// </summary>
    /// <param name="startIdx">New vehicle start position index</param>
    /// <param name="snap">Whether to snap vehicle to required position after moving</param>
    private void MoveToIndex(int startIdx, bool snap = true)
    {
        PositionIdxStart = startIdx;

        if (snap)
        {
            Snap();
        }
    }

    /// <summary>
    /// Snap vehicle to its required position
    /// </summary>
    public void Snap()
    {
        transform.position = GetCenterWorldPosition();
    }

    /// <summary>
    /// Toggle whether vehicle is visible (active or inactive)
    /// </summary>
    public void ToggleVisibility(bool visible)
    {
        gameObject.SetActive(visible);
    }

    /// <summary>
    /// Raise a vehicle when grabbed
    /// </summary>
    public void GrabStart()
    {
        vehicleTransform.localPosition = restingPosition + Vector3.up * 0.1f;
    }

    /// <summary>
    /// Drop a vehicle after releasing
    /// </summary>
    public void GrabEnd()
    {
        vehicleTransform.localPosition = restingPosition;
    }
    #endregion
}
