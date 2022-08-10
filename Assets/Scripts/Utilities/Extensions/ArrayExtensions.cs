using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions
{
    #region Custom Methods
    public static void ForEach<T>(this T[] list, Action<T> callback)
    {
        for (int i = 0; i < list.Length; i++)
        {
            callback(list[i]);
        }
    }

    public static void ForEach<T>(this T[] list, Action<T, int> callback)
    {
        for (int i = 0; i < list.Length; i++)
        {
            callback(list[i], i);
        }
    }
    #endregion
}
