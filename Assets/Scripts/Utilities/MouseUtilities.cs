using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MouseUtilities
{
    /// <summary>
    /// Get the horizontal angle to the mouse position (using source y-level)
    /// </summary>
    /// <param name="sourcePosition">Source position</param>
    /// <param name="mousePosition">Mouse position (necessary since there are two input systems)</param>
    /// <returns>Horizontal angle to mouse position</returns>
    public static float GetHorizontalAngleToMouse(Vector3 sourcePosition, Vector3 mousePosition)
    {
        return GetHorizontalAngleToMouse(mousePosition, sourcePosition, sourcePosition.y);
    }

    /// <summary>
    /// Get the horizontal angle to the mouse position
    /// </summary>
    /// <param name="sourcePosition">Source position</param>
    /// <param name="mousePosition">Mouse position (necessary since there are two input systems)</param>
    /// <param name="yLevel">Y-level for mouse plane</param>
    /// <returns>Horizontal angle to mouse position</returns>
    public static float GetHorizontalAngleToMouse(Vector3 sourcePosition, Vector3 mousePosition, float yLevel)
    {
        Vector3 mousePoint = GetMouseLookPoint(mousePosition);

        return GetHorizontalAngleToTarget(sourcePosition, mousePoint);
    }

    /// <summary>
    /// Get the horizontal angle to a target position
    /// </summary>
    /// <param name="sourcePosition">Source position</param>
    /// <param name="targetPosition">Target position</param>
    /// <returns>Horizontal angle to target position</returns>
    public static float GetHorizontalAngleToTarget(Vector3 sourcePosition, Vector3 targetPosition)
    {
        Vector3 offset = new Vector3(targetPosition.x - sourcePosition.x, 0, targetPosition.z - sourcePosition.z);

        return Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Get the mouse look point on a given y-plane
    /// </summary>
    /// <param name="mousePosition">Mouse position (necessary since there are two input systems)</param>
    /// <param name="planeYPosition">Y-level for mouse point plane</param>
    /// <returns>Mouse look point</returns>
    public static Vector3 GetMouseLookPoint(Vector3 mousePosition, float planeYPosition = 0)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Vector3 planeInPoint = new Vector3(0, planeYPosition, 0);
        Plane ground = new Plane(Vector3.up, planeInPoint);
        float rayDistance;

        // QUESTION: What happens if there is no raycast (should always be with plane)?
        ground.Raycast(ray, out rayDistance);
        return ray.GetPoint(rayDistance);
    }
}

