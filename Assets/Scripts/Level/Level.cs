using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}

[Serializable]
public class Level
{
    #region Properties
    public Difficulty Difficulty;
    /// <summary>
    /// Minimum number of necessary moves to solve
    /// </summary>
    public int Moves;
    /// <summary>
    /// Car layout (string form)
    /// <list type="bullet">
    ///     <item>
    ///         <term><c>A</c></term>
    ///         <description>Player car</description>
    ///     </item>
    ///     <item>
    ///         <term><c>.</c></term>
    ///         <description>Open space</description>
    ///     </item>
    ///     <item>
    ///         <term><c>[B-Z]</c></term>
    ///         <description>Regular cars</description>
    ///     </item>
    /// </list>
    /// <example>
    ///     <code>BB...CD..E.CDAAE.CD..E..F...GGF.HHH.</code>
    /// </example>
    /// </summary>
    public string LayoutString;
    #endregion
}
