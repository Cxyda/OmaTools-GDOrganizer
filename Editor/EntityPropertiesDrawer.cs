using System;
using System.Collections.Generic;
using Plugins.O.M.A.Games.GDOrganizer.Editor.Utils;
using Plugins.O.M.A.Games.GDOrganizer.GameDesignDefinition;
using Plugins.O.M.A.Games.GDOrganizer.Runtime.Entity;
using UnityEditor;
using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor
{
    [CustomPropertyDrawer(typeof(EntityProperties))]
    public class EntityPropertiesDrawer : PropertyDrawer
    {
        private int _enumLength;
        private EntityProperty[] _groupValues;

        bool _listExpanded = true;

        private int _propertyCount;
        private SerializedProperty _listProperty;
        private SerializedProperty _entityType;

        private List<EntityProperty> _setProperties = new List<EntityProperty>();
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _setProperties.Clear();
            _listProperty = property.FindPropertyRelative("Properties");

            _propertyCount = _listProperty.arraySize;

            Rect header = position.ShrinkFromTop(EditorGUIUtility.singleLineHeight);
            Rect footer = position.ShrinkFromBottom(EditorGUIUtility.singleLineHeight * 2f);
            GatherPropertyData();

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(position, label, property);

            
            float halfWidth = position.width / 2f;

            _listExpanded = EditorGUI.Foldout(header, _listExpanded, "Show assigned Properties", true, EditorStyles.foldout);

            if (_listExpanded)
            {
                position = position.MoveDown(EditorGUIUtility.singleLineHeight);
                DrawEnumList(position);
               
                if(GUI.Button(footer.ShrinkFromLeft(halfWidth), "Select None"))
                {
                    for (int i = _propertyCount-1; i >= 0; i--)
                    {
                        _listProperty.DeleteArrayElementAtIndex(i);
                    }
                    _setProperties.Clear();
                }
                //footerRect.x += halfWidth + 5;
                if(GUI.Button(footer.ShrinkFromRight(halfWidth), "Select All"))
                {
                    for (int i = 0; i < _enumLength; i++)
                    {
                        // use the index of the enum here!
                        AddItemAtIndex(i, i);
                    }
                }

            }


            EditorGUI.EndProperty();
            EditorGUI.EndChangeCheck();

        }

        private void GatherPropertyData()
        {
            _groupValues = Enum.GetValues(typeof(EntityProperty)) as EntityProperty[];
            _enumLength = _groupValues.Length;
            for (int i = 0; i < _propertyCount; i++)
            {
                _setProperties.Add((EntityProperty)_listProperty.GetArrayElementAtIndex(i).intValue);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_listExpanded)
            {
                float propertyListHeight =
                    (Mathf.Ceil(_enumLength + 2) / 2f) * (EditorGUIUtility.singleLineHeight) + 20;
                return propertyListHeight + EditorGUIUtility.singleLineHeight * 2;
            }
            else
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }

        private void DrawEnumList(Rect rect)
        {
            float halfWidth = rect.width / 2f;

            bool[] buttonPressed = new bool[_enumLength];
            var row = rect.ShrinkFromTop(EditorGUIUtility.singleLineHeight);

            for (var i = 0; i < _enumLength; i++)
            {
                bool listContainsProperty = _setProperties.Contains(_groupValues[i]);
                buttonPressed[i] = listContainsProperty;
                Rect buttonRect;
                if (i % 2 == 0)
                {
                    buttonRect = row.ShrinkFromLeft(halfWidth);
                }
                else
                {
                    buttonRect = row.ShrinkFromRight(halfWidth);
                    row = row.MoveDown(EditorGUIUtility.singleLineHeight);
                }

                buttonPressed[i] = GUI.Toggle(buttonRect, buttonPressed[i], _groupValues[i].ToString(), EditorStyles.miniButtonMid);
                bool hasBeenChanged = listContainsProperty != buttonPressed[i];

                if (hasBeenChanged)
                {
                    if (listContainsProperty)
                    {
                        DeleteItem(i);
                    }
                    else
                    {
                        // use the index of the enum here!
                        AddItemAtIndex(i, _propertyCount);
                    }
                }
            }
        }

        private void AddItemAtIndex(int enumItemIndex, int index)
        {
            _setProperties.Insert(index, _groupValues[enumItemIndex]);
            _listProperty.InsertArrayElementAtIndex(index);

            _listProperty.GetArrayElementAtIndex(index).intValue = (int)_groupValues[enumItemIndex];
        }

        private void DeleteItem(int enumItemIndex)
        {
            int index = _setProperties.IndexOf((EntityProperty)_groupValues[enumItemIndex]);
            _listProperty.DeleteArrayElementAtIndex(index);
            _setProperties.RemoveAt(index);
        }

    }
    
}
