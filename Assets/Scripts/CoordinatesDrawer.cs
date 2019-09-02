using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TileCoordinates))]
public class CoordinatesDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TileCoordinates coordinates = new TileCoordinates(
            property.FindPropertyRelative("x").intValue, 
            property.FindPropertyRelative("z").intValue);

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, coordinates.ToString());
    }
}