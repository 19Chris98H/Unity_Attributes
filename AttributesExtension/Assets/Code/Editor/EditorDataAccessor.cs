using System;
using System.Linq;
using System.Reflection;

public static class EditorDataAccessor
{
    public static EditorData GetEditorData()
    {
        var editorAssembly = AppDomain.CurrentDomain.GetAssemblies().First( a => a.FullName.StartsWith("Assembly-CSharp,"));
        var utilityType = editorAssembly.GetTypes().FirstOrDefault( t => t.FullName.Contains("EditorData"));
        return utilityType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as EditorData;
    }
}
