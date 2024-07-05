using UnityEditor;
using UnityEngine;

namespace JoyNow.SLG.Editor
{
    [CustomPropertyDrawer(typeof(CellCoordinates))]
    public class CellCoordinatesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var coordinates = new CellCoordinates(
                property.FindPropertyRelative("x").intValue,
                property.FindPropertyRelative("z").intValue
            );
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            GUI.Label(position, coordinates.ToString());
        }
    }
}