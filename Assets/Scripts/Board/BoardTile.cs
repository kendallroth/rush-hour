using Drawing;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardTile : MonoBehaviourGizmos
{
    #region Attributes
    #endregion


    #region Properties
    [ShowInInspector]
    [ReadOnly]
    public Coordinates Coordinates { get; private set; }
    public int Index => Coordinates.Y * Board.SIZE + Coordinates.X;
    #endregion


    #region Unity Methods
    #endregion


    #region Custom Methods
    public void Init(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }
    #endregion


    #region Debug Methods
    public override void DrawGizmos()
    {
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        Vector3 position = transform.position + Vector3.up * 0.02f;

        if (DebugManager.Instance.DrawTileCoordinates)
        {
            Draw.Label3D(new float3(position.x, position.y, position.z), rotation, $"{Coordinates.X},{Coordinates.Y}", 0.2f, LabelAlignment.Center);
            Draw.Label3D(new float3(position.x, position.y, position.z - 0.25f), rotation, $"{Index}", 0.13f, LabelAlignment.Center);
        }

        if (DebugManager.Instance.DrawTileBorders)
        {
            Draw.SolidCircle(new float3(position.x - 0.5f, position.y, position.z - 0.5f), Vector3.up, 0.025f);
            Draw.SolidCircle(new float3(position.x + 0.5f, position.y, position.z - 0.5f), Vector3.up, 0.025f);
            Draw.SolidCircle(new float3(position.x - 0.5f, position.y, position.z + 0.5f), Vector3.up, 0.025f);
            Draw.SolidCircle(new float3(position.x + 0.5f, position.y, position.z + 0.5f), Vector3.up, 0.025f);
        }
    }
    #endregion
}
