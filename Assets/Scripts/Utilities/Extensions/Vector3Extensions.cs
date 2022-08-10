using UnityEngine;

public static class Vector3Extensions
{
    #region Custom Methods
    /// <summary>
    /// Get the normalized direction to a target
    /// </summary>
    /// <param name="origin">Origin point</param>
    /// <param name="target">Target point</param>
    /// <returns>Direction to a target</returns>
    public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
    {
        return (target - origin).normalized;
    }

    /// <summary>
    /// Get the clamped rotation from one point to another.
    /// <br /><br />
    /// Taken from: https://forum.unity.com/threads/how-do-i-clamp-a-quaternion.370041/
    /// </summary>
    /// <param name="direction">Current direction</param>
    /// <param name="originalDirection">Original direction (required for clamping)</param>
    /// <param name="maxAngle">Maximum rotation angle</param>
    /// <returns>Clamped rotation from one point to another</returns>
    public static Vector3 ClampedDirectionTo(this Vector3 direction, Vector3 originalDirection, float maxAngle)
    {
        float angle = Vector3.Angle(originalDirection, direction);

        // Angles exceeding the clamp value must be recalulated to fit within the clamp
        if (Mathf.Abs(angle) > maxAngle)
        {
            direction.Normalize();
            originalDirection.Normalize();

            // Clamped direction is "close" to the intended direction, but within the clamp values
            Vector3 rotation = (direction - originalDirection) / angle;
            return (rotation * maxAngle) + originalDirection;
        }

        return direction;
    }

    /// <summary>
    /// Get the rotation from a direction
    /// </summary>
    /// <param name="direction">Normalized direction vector</param>
    /// <returns>Rotation from direction</returns>
    public static Quaternion RotationTo(this Vector3 direction)
    {
        return Quaternion.LookRotation(direction);
    }

    /// <summary>
    /// Get the rotation from one point to another
    /// </summary>
    /// <param name="origin">Origin point</param>
    /// <param name="target">Target point</param>
    /// <returns>Rotation from one point to another</returns>
    public static Quaternion RotationTo(this Vector3 origin, Vector3 target)
    {
        Vector3 direction = DirectionTo(origin, target);
        return RotationTo(direction);
    }

    /// <summary>
    /// Get the clamped rotation from a direction
    /// </summary>
    /// <param name="direction">Current direction</param>
    /// <param name="originalDirection">Original direction (required for clamping)</param>
    /// <param name="maxAngle">Maximum rotation angle</param>
    /// <returns>Clamped rotation from direction</returns>
    public static Quaternion ClampedRotationTo(this Vector3 direction, Vector3 originalDirection, float maxAngle)
    {
        Vector3 clampedDirection = ClampedDirectionTo(direction, originalDirection, maxAngle);

        return RotationTo(clampedDirection);
    }

    /// <summary>
    /// Find a point at an arbitray distance between two other points
    /// </summary>
    /// <param name="startPoint">Starting point</param>
    /// <param name="endPoint">Ending point</param>
    /// <param name="distance">Distance from an end</param>
    /// <param name="useEndPoint">Whether end point is used for calculating distance</param>
    /// <returns>Point at distance between two other points</returns>
    public static Vector3 PointAlongLine(Vector3 startPoint, Vector3 endPoint, float distance = 0.0f, bool useEndPoint = false)
    {
        Vector3 direction = (endPoint - startPoint).normalized;

        // Point along line can be calculated from either end
        return useEndPoint ? endPoint - (distance * direction) : startPoint + (distance * direction);
    }

    /// <summary>
    /// Find the closest point on an infinite line
    /// </summary>
    /// <param name="startPoint">Starting point</param>
    /// <param name="endpoint">Ending point</param>
    /// <param name="point">Input point to map along start/end</param>
    /// <remarks>Source: https://stackoverflow.com/a/62872782/4206438</remarks>
    /// <returns>Closest point along start/end line</returns>
    public static Vector3 ClosestPointOnLineInFinite(Vector3 startPoint, Vector3 endpoint, Vector3 point)
    {
        return startPoint + Vector3.Project(point - startPoint, endpoint - startPoint);
    }

    /// <summary>
    /// Find the closest point on a finite line
    /// </summary>
    /// <param name="startPoint">Starting point</param>
    /// <param name="endpoint">Ending point</param>
    /// <param name="point">Input point to map between start/end</param>
    /// <remarks>Source: https://stackoverflow.com/a/62872782/4206438</remarks>
    /// <returns>Closest point between start/end</returns>
    public static Vector3 ClosestPointOnLineFinite(Vector3 startPoint, Vector3 endpoint, Vector3 point)
    {
        Vector3 lineDirection = endpoint - startPoint;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();
        float projectLength = Mathf.Clamp(Vector3.Dot(point - startPoint, lineDirection), 0f, lineLength);
        return startPoint + lineDirection * projectLength;
    }
    #endregion
}


