using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    #region Custom Methods
    public static void DestroySelf(this GameObject obj)
    {
        // Assets are destroyed differently while playing vs in the editor
        if (Application.isPlaying)
        {
            MonoBehaviour.Destroy(obj);
        }
        else
        {
            MonoBehaviour.DestroyImmediate(obj);
        }
    }

    /// <summary>
    /// Perform delegate function after timeout
    /// </summary>
    /// <param name="time">Timeout duration</param>
    /// <param name="callback">Delegate callback</param>
    /// <returns></returns>
    private static IEnumerator SetTimeout(this MonoBehaviour mono, float time, Action callback)
    {
        yield return new WaitForSeconds(time);

        callback();
    }

    /// <summary>
    /// Enable waiting for a timeout before performing a delegate function
    /// </summary>
    /// <param name="seconds">Timeout duration</param>
    /// <param name="callback">Delegate callback</param>
    public static Coroutine Wait(this MonoBehaviour mono, float seconds, Action callback)
    {
        return mono.StartCoroutine(SetTimeout(mono, seconds, callback));
    }
    #endregion
}
