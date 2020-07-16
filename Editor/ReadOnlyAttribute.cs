using Plugins.O.M.A.Games.GDOrganizer.Runtime.Utils;
using UnityEditor;
using UnityEngine;

//**********************************************************//
//*     Copyright Daniel "Cxyda" Steegmüller 2015           //
//*                 http://www.polybeast.de                 //
//*       Project:  "Die Zunft" (WorkingTitle)  			//
//**********************************************************//  

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}