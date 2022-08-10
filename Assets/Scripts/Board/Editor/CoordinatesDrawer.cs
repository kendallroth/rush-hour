using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Coordinates))]
public class CoordinatesDrawer : PropertyDrawer
{
    #region Unity Methods
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Coordinates coordinates = new Coordinates(
            property.FindPropertyRelative("X").intValue,
            property.FindPropertyRelative("Y").intValue
        );

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, coordinates.ToString());
    }
    #endregion
}
