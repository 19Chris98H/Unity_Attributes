using UnityEditor;

public static class SerializedPropertyExtentions
{
    public static T GetValue<T>(this SerializedProperty property)
    {
        return ReflectionUtility.GetNestedObject<T>(property.serializedObject.targetObject, property.propertyPath);
    }
}