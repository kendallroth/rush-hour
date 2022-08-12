using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Temporary subclass to handle fading child button images with default transitions
/// </summary>
/// <remarks>Source: https://forum.unity.com/threads/tint-multiple-targets-with-single-button.279820/#post-5092682</remarks>
public class MultiImageButton : Button
{
    #region Unity Methods
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        var targetColor =
            state == SelectionState.Disabled ? colors.disabledColor :
            state == SelectionState.Highlighted ? colors.highlightedColor :
            state == SelectionState.Normal ? colors.normalColor :
            state == SelectionState.Pressed ? colors.pressedColor :
            state == SelectionState.Selected ? colors.selectedColor : Color.white;

        foreach (var graphic in GetComponentsInChildren<Graphic>())
        {
            graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }
    }
    #endregion
}

