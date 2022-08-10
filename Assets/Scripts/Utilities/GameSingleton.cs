using Drawing;
using UnityEngine;

/// <summary>
/// Enable singleton pattern for one-off classes
/// <br /><br />
/// Taken from: https://gist.github.com/mstevenson/4325117 <br />
/// Possible idea: https://gist.github.com/michidk/640765fc570220333ac1
/// </summary>
/// <typeparam name="T">Type of Instance provided by singleton</typeparam>
public class GameSingleton<T> : MonoBehaviourGizmos where T : Component
{
    #region Properties
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.LogError("An instance of " + typeof(T) + " is needed in the scene but not found");

                }
            }

            return instance;
        }
    }
    #endregion
}

