using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HideIfAttribute))]
public class HideIfPropertyDrawer : PropertyDrawer
{
    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var hideIf = attribute as HideIfAttribute;
        var comparedField = property.serializedObject.FindProperty(hideIf.comparedPropertyName);

        // Get the value of the compared field.
        object comparedFieldValue = comparedField.GetValue<object>();

        // References to the values as numeric types.
        NumericType numericComparedFieldValue = null;
        NumericType numericComparedValue = null;

        try
        {
            // Try to set the numeric types.
            numericComparedFieldValue = new NumericType(comparedFieldValue);
            numericComparedValue = new NumericType(hideIf.comparedValue);
        }
        catch (NumericTypeExpectedException)
        {
            // This place will only be reached if the type is not a numeric one. If the comparison type is not valid for the compared field type, log an error.
            if (hideIf.comparisonType != ComparisonType.Equals && hideIf.comparisonType != ComparisonType.NotEqual)
            {
                Debug.LogError("The only comparison types available to type '" + comparedFieldValue.GetType() + "' are Equals and NotEqual. (On object '" + property.serializedObject.targetObject.name + "')");
                return;
            }
        }

        
        var conditionMet = false;

        // Compare the values to see if the condition is met.
        switch (hideIf.comparisonType)
        {
            case ComparisonType.Equals:
                if (comparedFieldValue.Equals(hideIf.comparedValue))
                    conditionMet = true;
                break;

            case ComparisonType.NotEqual:
                if (!comparedFieldValue.Equals(hideIf.comparedValue))
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
        }

        // The height of the property should be defaulted to the default height.
        propertyHeight = base.GetPropertyHeight(property, label);

        // If the condition is not met, simply draw the field. Else...
        if (!conditionMet)
        {
            EditorGUI.PropertyField(position, property);
        }
        else
        {
            //...check if the disabling type is read only. If it is, draw it disabled, else, set the height to zero.
            if (hideIf.disablingType == DisablingType.ReadOnly)
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property);
                GUI.enabled = true;
            }
            else
            {
                propertyHeight = 0f;
            }
        }
    }
}