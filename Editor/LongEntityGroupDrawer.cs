using System;
using Logic;
using UnityEditor;
using UnityEngine;

namespace Services.EntityService.Editor
{
    [CustomPropertyDrawer(typeof(LongEntityGroup))]
    public class LongEntityGroupDrawer : PropertyDrawer
    {
        private int _enumLength;
        private string[] _groupNames;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            long groupFlags = property.FindPropertyRelative("Value").longValue;

            EditorGUI.BeginProperty(position, label, property);
            position.y += 5;

            
            
            _groupNames = Enum.GetNames(typeof(EntityGroup));
            _enumLength = _groupNames.Length;
            long buttonsIntValue = 0L;
            var buttonPressed = new bool[_enumLength];
            position.width = EditorGUIUtility.currentViewWidth;
            var indentation = 20;
            float halfWidth = (position.width - indentation ) / 2 - 5;
            float buttonHeight = EditorGUIUtility.singleLineHeight + 1;
            EditorGUI.BeginChangeCheck ();
            Rect buttonPos = position;
            for (var i = 0; i < _enumLength; i++) {
 
                // Check if the button is/was pressed 
                if ( ( groupFlags & (1L << i) ) == 1L << i ) {
                    buttonPressed[i] = true;
                }

                var buttonPosX = position.x + (i % 2) * (halfWidth + 5);
                var buttonPosY = position.y + (buttonHeight + 1) * (i/2);
                buttonPos = new Rect (buttonPosX, buttonPosY, halfWidth, buttonHeight);
 
                buttonPressed[i] = GUI.Toggle(buttonPos, buttonPressed[i], _groupNames[i], EditorStyles.miniButtonMid);
 
                if (buttonPressed[i])
                    buttonsIntValue += 1L << i;
            }

            var footerRect = new Rect(position.x, position.y + Mathf.Ceil(_enumLength/2f) * (buttonHeight+1) + 5, halfWidth, buttonHeight);
            if(GUI.Button(footerRect, "Select None"))
            {
                buttonsIntValue = 0L;
            }

            footerRect.x += halfWidth + 5;
            if(GUI.Button(footerRect, "Select All"))
            {
                buttonsIntValue = (long) (Mathf.Pow(2, _enumLength) - 1) ;
            }

            footerRect.y += 22;
            footerRect.width *= 2;
            //BitArray bits = new BitArray(System.BitConverter.GetBytes(buttonsIntValue));
            //GUI.Label(position, bits.ToBitString());

            if (EditorGUI.EndChangeCheck()) {

                property.FindPropertyRelative("Value").longValue = buttonsIntValue;

            }
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Mathf.Ceil((_enumLength+1)/2f)* (EditorGUIUtility.singleLineHeight + 3) + 10;
        }

    }
    
}
