using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideIfAttribute))]
public class HideIfPropertyDrawer : PropertyDrawer
{
    private float _propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return _propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var hideIf = attribute as HideIfAttribute;
        var comparedField = property.serializedObject.FindProperty(hideIf?.ComparedPropertyName);

        var comparedFieldValue = comparedField.GetValue<object>();

        NumericType numericComparedFieldValue = null;
        NumericType numericComparedValue = null;

        try
        {
            numericComparedFieldValue = new NumericType(comparedFieldValue);
            numericComparedValue = new NumericType(hideIf?.ComparedValue);
        }
        catch (NumericTypeExpectedException)
        {
            if (hideIf?.ComparisonType != ComparisonType.Equals && hideIf?.ComparisonType != ComparisonType.NotEqual)
            {
                Debug.LogError("The only comparison types available to type '" + comparedFieldValue.GetType() + "' are Equals and NotEqual. (On object '" + property.serializedObject.targetObject.name + "')");
                return;
            }
        }

        
        var conditionMet = false;

        // Compare the values to see if the condition is met.
        switch (hideIf?.ComparisonType)
        {
            case ComparisonType.Equals:
                if (comparedFieldValue.Equals(hideIf.ComparedValue))
                    conditionMet = true;
                break;

            case ComparisonType.NotEqual:
                if (!comparedFieldValue.Equals(hideIf.ComparedValue))
                    conditionMet = true;
                break;

            case ComparisonType.GreaterThan:
                if (numericComparedFieldValue > numericComparedValue)
                    conditionMet = true;
                break;

            case ComparisonType.SmallerThan:
                if (numericComparedFieldValue < numericComparedValue)
                    conditionMet = true;
                break;

            case ComparisonType.SmallerOrEqual:
                if (numericComparedFieldValue <= numericComparedValue)
                    conditionMet = true;
                break;

            case ComparisonType.GreaterOrEqual:
                if (numericComparedFieldValue >= numericComparedValue)
                    conditionMet = true;
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }

        _propertyHeight = base.GetPropertyHeight(property, label);

        if (!conditionMet)
        {
            EditorGUI.PropertyField(position, property);
        }
        else
        {
            if (hideIf.DisablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property);
                GUI.enabled = true;
            }
            else
            {
                _propertyHeight = 0f;
            }
        }
    }
}