using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyRuntimeAttribute))]
public class ReadOnlyRuntimeDrawer : ReadOnlyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorApplication.isPlaying ? base.GetPropertyHeight(property, label) : 0;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (EditorApplication.isPlaying)
            base.OnGUI(position, property, label);
    }
}
