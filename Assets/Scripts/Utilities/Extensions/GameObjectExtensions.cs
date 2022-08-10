using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    #region Custom Methods
    /// <summary>
    /// Clear a game object's children
    /// </summary>
    /// <param name="objectTransform">Parent game object</param>
    public static void RemoveChildren(this GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        int childrenCount = transform.childCount;

        for (int i = childrenCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).gameObject.DestroySelf();

            // Assets are destroyed differently while playing vs in the editor
            /*if (Application.isPlaying)
            {
                MonoBehaviour.Destroy(transform.GetChild(i).gameObject);
            }
            else
            {
                MonoBehaviour.DestroyImmediate(transform.GetChild(i).gameObject);
            }*/
        }
    }



    /// <summary>
    /// Remove component from an object
    /// </summary>
    /// <param name="components">Component to remove</param>
    public static void RemoveComponent(this GameObject gameObject, Component component)
    {
        if (Application.isPlaying)
        {
            MonoBehaviour.Destroy(component);
        }
        else
        {
            MonoBehaviour.DestroyImmediate(component);
        }
    }
    #endregion
}
