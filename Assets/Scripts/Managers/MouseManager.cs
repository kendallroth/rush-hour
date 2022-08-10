using Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

internal struct MouseSelection
{
    public Vehicle Vehicle;
    /// <summary>
    /// Initial mouse position (where drag started)
    /// </summary>
    public Vector3 DragStart;
    /// <summary>
    /// Initial mouse position offset from selected vehicle center
    /// </summary>
    public Vector3 DragOffsetStart;
    /// <summary>
    /// Movement boundaries for vehicle
    /// </summary>
    public VehicleMoveBounds MoveBounds;
}

public class MouseManager : GameSingleton<MouseManager>
{
    #region Attributes
    [SerializeField]
    private LayerMask tileLayer;
    [SerializeField]
    private LayerMask vehicleLayer;
    #endregion


    #region Properties
    #endregion

    private PlayerInput playerInput;
    private float boardTileSize => BoardGenerator.Instance.TileSize;

    /// <summary>
    /// Cached mouse selection data (vehicle, drag positioning, bounds, etc)
    /// </summary>
    private MouseSelection? mouseSelection = null;


    #region Unity Methods
    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Mouse.Enable();
        playerInput.Mouse.Click.started += MouseClick;
        playerInput.Mouse.Click.canceled += MouseRelease;
        playerInput.Mouse.Move.performed += MouseMove;
    }

    private void OnDisable()
    {
        playerInput.Mouse.Disable();
        playerInput.Mouse.Click.started -= MouseClick;
        playerInput.Mouse.Click.canceled -= MouseRelease;
        playerInput.Mouse.Move.performed -= MouseMove;
    }
    #endregion


    #region Custom Methods
    private void MouseClick(InputAction.CallbackContext ctx)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 100f, vehicleLayer))
        {
            ClearSelection();
            return;
        }

        Vehicle vehicle = hit.collider.GetComponentInParent<Vehicle>();
        if (vehicle == null) return;

        Vector3 dragStart = MouseUtilities.GetMouseLookPoint(mousePosition, vehicle.transform.position.y);
        var moveBounds = Board.Instance.GetMoveBounds(vehicle);

        // Cache mouse selection data to avoid calculating every mouse movement
        mouseSelection = new MouseSelection
        {
            DragStart = dragStart,
            // Need to track offset between initial mouse position and center of selected vehicle
            //   to avoid snapping when moving (would otherwise always snap vehicle center to mouse).
            DragOffsetStart = dragStart - vehicle.transform.position,
            MoveBounds = moveBounds,
            Vehicle = vehicle,
        };
    }

    private void MouseMove(InputAction.CallbackContext ctx)
    {
        if (!mouseSelection.HasValue) return;

        Vehicle selectedVehicle = mouseSelection.Value.Vehicle;
        VehicleMoveBounds moveBounds = mouseSelection.Value.MoveBounds;

        // Gridlocked vehicles can never move
        if (moveBounds.GridLocked) return;

        // Mouse positions/dragging should use the y-plane of the selected vehicle
        Vector2 mousePositionRaw = ctx.ReadValue<Vector2>();
        float vehicleYPosition = selectedVehicle.transform.position.y;
        Vector3 mousePosition = MouseUtilities.GetMouseLookPoint(mousePositionRaw, vehicleYPosition);

        // Calculate closest point to the mouse between the vehicle movement position bounds,
        //   to prevent dragging the vehicle past its bounds.
        Vector3 dragPosition = Vector3Extensions.ClosestPointOnLineFinite(moveBounds.CenterPositionMin, moveBounds.CenterPositionMax, mousePosition - mouseSelection.Value.DragOffsetStart);

        selectedVehicle.transform.position = dragPosition;
    }

    private void MouseRelease(InputAction.CallbackContext ctx)
    {
        if (!mouseSelection.HasValue) return;

        Vehicle selectedVehicle = mouseSelection.Value.Vehicle;
        VehicleMoveBounds moveBounds = mouseSelection.Value.MoveBounds;

        Vector3 vehiclePositionStart = selectedVehicle.GetCenterWorldPosition();
        Vector3 vehiclePositionEnd = selectedVehicle.transform.position;
        Vector3 moveDelta = vehiclePositionEnd - vehiclePositionStart;
        float moveDistance = selectedVehicle.Orientation == Orientation.HORIZONTAL ? moveDelta.x : -moveDelta.z;

        float stepsRaw = Mathf.Round(moveDistance / boardTileSize);
        int steps = (int)stepsRaw.Clamp(moveBounds.StepsBackward, moveBounds.StepsForward);

        // Only perform move if vehicle actually moved by at least a step, otherwise return to original position
        if (steps != 0)
        {
            GameManager.Instance.PerformMove(selectedVehicle, steps);
        }
        else
        {
            selectedVehicle.Snap();
        }

        ClearSelection();
    }

    private void ClearSelection()
    {
        mouseSelection = null;
    }
    #endregion


    #region Debug Methods
    public override void DrawGizmos()
    {
        if (!mouseSelection.HasValue) return;

        VehicleMoveBounds moveBounds = mouseSelection.Value.MoveBounds;

        if (!moveBounds.GridLocked)
        {
            Draw.SolidCircle(moveBounds.StartPositionMin + Vector3.up * 0.1f, Vector3.up, 0.1f, Color.yellow);
            Draw.SolidCircle(moveBounds.StartPositionMax + Vector3.up * 0.1f, Vector3.up, 0.1f, Color.yellow);
            Draw.Line(moveBounds.StartPositionMin + Vector3.up * 0.1f, moveBounds.StartPositionMax + Vector3.up * 0.1f, Color.yellow);

            //var moveBoundsCenters = Board.Instance.GetMoveBoundsCenters(selectedVehicle);
            Draw.SolidCircle(moveBounds.CenterPositionMin + Vector3.up * 0.1f, Vector3.up, 0.05f, Color.red);
            Draw.SolidCircle(moveBounds.CenterPositionMax + Vector3.up * 0.1f, Vector3.up, 0.05f, Color.red);
        }
    }
    #endregion
}
