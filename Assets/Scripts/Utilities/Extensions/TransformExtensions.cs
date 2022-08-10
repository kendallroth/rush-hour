using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Get the normalized direction to a target
    /// </summary>
    /// <param name="origin">Origin transform</param>
    /// <param name="target">Target transform</param>
    /// <returns>Direction to a target</returns>
    public static Vector3 DirectionTo(this Transform origin, Transform target)
    {
        return origin.position.DirectionTo(target.position);
    }

    /// <summary>
    /// Transform 'forEachChild' callback
    /// </summary>
    /// <param name="child">Transform child game object</param>
    /// <param name="index">Child index</param>
    public delegate void TransformChildCallback(GameObject child, int index);

    /// <summary>
    /// Perform an action for each child game object
    /// </summary>
    /// <param name="childHandler">
    ///     Handler for each child
    ///     <param name="childHandler arg1">Text</param>
    ///     <param name="childHandler arg2">Test</param>
    /// </param>
    /// <param name="recursive">Whether to perform recursively</param>
    public static void ForEachChild(this Transform transform, TransformChildCallback childHandler, bool recursive = false)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);

            childHandler(childTransform.gameObject, i);

            if (recursive)
            {
                ForEachChild(childTransform, childHandler, recursive);
            }
        }
    }

    /// <summary>
    /// Clear a game object transform's children
    /// </summary>
    /// <param name="objectTransform">Parent game object transform</param>
    public static void RemoveChildren(this Transform transform)
    {
        transform.gameObject.RemoveChildren();
    }
}
