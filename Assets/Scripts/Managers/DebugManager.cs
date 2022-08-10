using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : GameSingleton<DebugManager>
{
    #region Attributes
    [Header("Tiles")]
    [OdinSerialize]
    public bool DrawTileBorders = true;
    [OdinSerialize]
    public bool DrawTileCoordinates = true;

    [Header("Vehicles")]
    [OdinSerialize]
    public bool DrawVehicleBounds = true;
    #endregion


    #region Properties
    #endregion


    #region Unity Methods
    #endregion


    #region Custom Methods
    #endregion
}
