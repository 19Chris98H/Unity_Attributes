using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DropdownListAttribute : PropertyAttribute
{
    public string Label { get; private set; }
    public string TargetObject { get; private set; }
    public string List { get; private set; }
    public bool UseEditorData { get; private set; }
    public bool IsIndex { get; private set; }

    public DropdownListAttribute(string label, string list, bool useEditorData = false, bool isIndex = false)
    {
        this.Label = label;
        this.List = list;
        this.UseEditorData = useEditorData;
        this.IsIndex = isIndex;
    }

    public DropdownListAttribute(string label, string targetObject, string list, bool isIndex = false)
    {
        this.Label = label;
        this.TargetObject = targetObject;
        this.List = list;
        this.IsIndex = isIndex;
    }
}
