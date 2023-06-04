using UnityEngine;
using System;

public enum DisablingType
{
    ReadOnly,
    Hide
}

public enum ComparisonType
{
    Equals = 1,
    NotEqual = 2,
    GreaterThan = 3,
    SmallerThan = 4,
    SmallerOrEqual = 5,
    GreaterOrEqual = 6
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class HideIfAttribute : PropertyAttribute
{
    public string ComparedPropertyName { get; private set; }
    public object ComparedValue { get; private set; }
    public ComparisonType ComparisonType { get; private set; }
    public DisablingType DisablingType { get; private set; }

    public HideIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType = ComparisonType.Equals, DisablingType disablingType = DisablingType.Hide)
    {
        this.ComparedPropertyName = comparedPropertyName;
        this.ComparedValue = comparedValue;
        this.ComparisonType = comparisonType;
        this.DisablingType = disablingType;
    }
}
