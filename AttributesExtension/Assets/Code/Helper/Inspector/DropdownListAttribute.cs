using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DropdownListAttribute : PropertyAttribute
{
    public string label { get; private set; }
    public string targetObject { get; private set; }
    public string list { get; private set; }
    public bool useEditorData { get; private set; }
    public bool isIndex { get; private set; }

    public DropdownListAttribute(string label, string list, bool useEditorData = false, bool isIndex = false)
    {
        this.label = label;
        this.list = list;
        this.useEditorData = useEditorData;
        this.isIndex = isIndex;
    }

    public DropdownListAttribute(string label, string targetObject, string list, bool isIndex = false)
    {
        this.label = label;
        this.targetObject = targetObject;
        this.list = list;
        this.isIndex = isIndex;
    }
}
