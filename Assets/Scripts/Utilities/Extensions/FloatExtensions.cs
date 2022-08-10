using UnityEngine;

public static class FloatExtensions
{
    /// <summary>
    /// Clamp a float between two values
    /// </summary>
    /// <param name="value">Current value</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <returns>Clamped value</returns>
    public static float Clamp(this float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }

    /// <summary>
    /// Map a value from a range to another range
    /// </summary>
    /// <param name="value">Current value</param>
    /// <param name="from1">Old range min</param>
    /// <param name="to1">New range min</param>
    /// <param name="from2">Old range max</param>
    /// <param name="to2">New range max</param>
    /// <returns>Value within the new range</returns>
    public static float Map(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    /// <summary>
    /// Map a percent into another range
    /// </summary>
    /// <param name="percent">Percent value</param>
    /// <param name="to1">New range min</param>
    /// <param name="to2">New range max</param>
    /// <returns>Value within the new range</returns>
    public static float MapFromPercent(this float percent, float to1, float to2)
    {
        return percent.Map(0f, 1f, to1, to2);
    }

    /// <summary>
    /// Map a value from a range into a percent
    /// </summary>
    /// <param name="value">Percent value</param>
    /// <param name="from1">Old range min</param>
    /// <param name="from2">Old range max</param>
    /// <returns>Percent value</returns>
    public static float MapToPercent(this float value, float from1, float from2)
    {
        return value.Map(from1, from2, 0f, 1f);
    }
}

