﻿using System;
using UnityEngine;

[Serializable]
public struct Coordinates
{
    public int X;
    public int Y;

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Convert a Coordinate to a Vector3
    /// </summary>
    public Vector3 ToVector3(float yLevel = 1.0f)
    {
        return new Vector3(X, yLevel, Y);
    } 

    public Vector3 GetWorldPosition(float tileSize)
    {
        return new Vector3(X * tileSize, 0, -Y * tileSize);
    }

    /// <summary>
    /// Convert a position index to a Coordinate
    /// </summary>
    public static Coordinates FromPositionIndex(int idx)
    {
        return new Coordinates(idx % Board.SIZE, idx / Board.SIZE);
    } 

    /// <summary>
    /// Convert a Coordinate to a position index
    /// </summary>
    public int ToPositionIndex()
    {
        return Y * Board.SIZE + X;
    } 

    /// <summary>
    /// Determine whether two coordinates are unequal
    /// </summary>
    /// <param name="first">First coordinate</param>
    /// <param name="second">Second coordinate</param>
    /// <returns>Whether two coordinates are unequal</returns>
    public static bool operator != (Coordinates first, Coordinates second)
    {
        return first.X != second.X || first.Y != second.Y;
    }

    /// <summary>
    /// Determine whether two coordinates are equal
    /// </summary>
    /// <param name="first">First coordinate</param>
    /// <param name="second">Second coordinate</param>
    /// <returns>Whether two coordinates are equal</returns>
    public static bool operator == (Coordinates first, Coordinates second)
    {
        return first.X == second.X && first.Y == second.Y;
    }

    /// <summary>
    /// Determine another coordinate is equal
    /// </summary>
    /// <param name="other">Other coordinate</param>
    /// <returns>Whether two coordinates are equal</returns>
    public override bool Equals(object other)
    {
        try
        {
            Coordinates otherCoordinate = (Coordinates) other;

            return otherCoordinate == this;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get the hash code from the object
    /// </summary>
    /// <returns>Object hash code</returns>
    public override int GetHashCode()
    {
        var hashCode = 1861411795;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Y.GetHashCode();
        return hashCode;
    }

    /// <summary>
    /// Represent a hex coordinate as a readable string
    /// </summary>
    /// <returns>Readable hex coordinate</returns>
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

