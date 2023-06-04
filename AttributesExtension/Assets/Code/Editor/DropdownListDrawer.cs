using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;

[CustomPropertyDrawer(typeof(DropdownListAttribute))]
public class DropdownListDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var dropdownListAttribute = attribute as DropdownListAttribute;

        if (property == null || (dropdownListAttribute.IsIndex && property.type != "int")) return;

        var list = new List<string>();
        if (dropdownListAttribute.TargetObject == null)
        {
            if (dropdownListAttribute.UseEditorData)
            {
                list = EditorDataAccessor.GetEditorData().GetField<List<string>>(dropdownListAttribute.List).ToList();
            }
            else
            {
                var listProperty = property.serializedObject.FindProperty(dropdownListAttribute.List);

                if (listProperty == null) return;

                for (var i = 0; i < listProperty.arraySize; i++)
                    list.Add((string)GetValue(listProperty.GetArrayElementAtIndex(i)));
            }
            
        }
        else
        {
            var targetProperty = property.serializedObject.FindProperty(dropdownListAttribute.TargetObject);
            if (targetProperty == null)
            {
                Debug.LogWarning($"Object {dropdownListAttribute.TargetObject} could not be found");
                return;
            }

            var targetObject = targetProperty.objectReferenceValue;
            if (targetObject == null)
                return;

            var listProperty = targetObject.GetType().GetField(dropdownListAttribute.List);
            if (listProperty == null)
            {
                Debug.LogWarning($"Property {dropdownListAttribute.List} of object {dropdownListAttribute.TargetObject} could not be found");
                return;
            }

            var value = listProperty.GetValue(targetObject);
            var objectList = ((IEnumerable)value).Cast<object>().ToList();
            list = objectList.Cast<string>().ToList();
        }

        if (list.Count < 1) return;

        var index = 0;
        var isValidIndex = true;
        if (dropdownListAttribute.IsIndex)
        {
            var value = property.intValue;
            isValidIndex = value > 0 && value < list.Count;
            if (isValidIndex)
                index = value;
        }
        else
        {
            var value = (string)GetValue(property);
            isValidIndex = list.Contains(value);
            if (isValidIndex) 
                index = list.IndexOf(value);
        }

        if (!isValidIndex) list.Insert(0, string.Empty);

        var selectedIndex = EditorGUI.Popup(position, dropdownListAttribute.Label, index, list.ToArray());

        if (!isValidIndex && selectedIndex == index) return;

        if (dropdownListAttribute.IsIndex)
            SetValue(property, selectedIndex);
        else
            SetValue(property, list[selectedIndex]);
    }

    private static object GetValue(SerializedProperty property)
    {
        switch (property.type)
        {
            case ("int"): return property.intValue;
            case ("long"): return property.longValue;
            case ("float"): return property.floatValue;
            case ("double"): return property.doubleValue;
            case ("string"): return property.stringValue;
            default: return null;
        };
    }

    private static void SetValue(SerializedProperty property, object value)
    {
        switch (property.type)
        {
            case ("int"): property.intValue = Convert.ToInt32(value); break;
            case ("long"): property.longValue = Convert.ToInt64(value); break;
            case ("float"): property.floatValue = Convert.ToSingle(value); break;
            case ("double"): property.doubleValue = Convert.ToDouble(value); break;
            case ("string"): property.stringValue = Convert.ToString(value); break;
        };
    }
}
