﻿using System;

public class NumericType<T> : IEquatable<NumericType<T>>
{
    private T value;
    private Type type;

    public NumericType(T obj)
    {
        if (!typeof(T).IsNumbericType())
        {
            // Something bad happened.
            throw new NumericTypeExpectedException("The type inputted into the NumericType generic must be a numeric type.");
        }
        type = typeof(T);
        value = obj;
    }

    public T GetValue()
    {
        return value;
    }

    public object GetValueAsObject()
    {
        return value;
    }

    public void SetValue(T newValue)
    {
        value = newValue;
    }

    public bool Equals(NumericType<T> other)
    {
        return this == other;
    }

    public override bool Equals(object obj)
    {
        if (obj != null && !(obj is NumericType<T>))
            return false;

        return Equals(obj);
    }

    public override int GetHashCode()
    {
        return GetValue().GetHashCode();
    }

    public override string ToString()
    {
        return GetValue().ToString();
    }

    /// <summary>
    /// Checks if the value of left is smaller than the value of right.
    /// </summary>
    public static bool operator <(NumericType<T> left, NumericType<T> right)
    {
        object leftValue = left.GetValueAsObject();
        object rightValue = right.GetValueAsObject();

        switch (Type.GetTypeCode(left.type))
        {
            case TypeCode.Byte:
                return (byte)leftValue < (byte)rightValue;

            case TypeCode.SByte:
                return (sbyte)leftValue < (sbyte)rightValue;

            case TypeCode.UInt16:
                return (ushort)leftValue < (ushort)rightValue;

            case TypeCode.UInt32:
                return (uint)leftValue < (uint)rightValue;

            case TypeCode.UInt64:
                return (ulong)leftValue < (ulong)rightValue;

            case TypeCode.Int16:
                return (short)leftValue < (short)rightValue;

            case TypeCode.Int32:
                return (int)leftValue < (int)rightValue;

            case TypeCode.Int64:
                return (long)leftValue < (long)rightValue;

            case TypeCode.Decimal:
                return (decimal)leftValue < (decimal)rightValue;

            case TypeCode.Double:
                return (double)leftValue < (double)rightValue;

            case TypeCode.Single:
                return (float)leftValue < (float)rightValue;
        }
        throw new NumericTypeExpectedException("Please compare valid numeric types with numeric generics.");
    }

    /// <summary>
    /// Checks if the value of left is greater than the value of right.
    /// </summary>
    public static bool operator >(NumericType<T> left, NumericType<T> right)
    {
        object leftValue = left.GetValueAsObject();
        object rightValue = right.GetValueAsObject();

        switch (Type.GetTypeCode(left.type))
        {
            case TypeCode.Byte:
                return (byte)leftValue > (byte)rightValue;

            case TypeCode.SByte:
                return (sbyte)leftValue > (sbyte)rightValue;

            case TypeCode.UInt16:
                return (ushort)leftValue > (ushort)rightValue;

            case TypeCode.UInt32:
                return (uint)leftValue > (uint)rightValue;

            case TypeCode.UInt64:
                return (ulong)leftValue > (ulong)rightValue;

            case TypeCode.Int16:
                return (short)leftValue > (short)rightValue;

            case TypeCode.Int32:
                return (int)leftValue > (int)rightValue;

            case TypeCode.Int64:
                return (long)leftValue > (long)rightValue;

            case TypeCode.Decimal:
                return (decimal)leftValue > (decimal)rightValue;

            case TypeCode.Double:
                return (double)leftValue > (double)rightValue;

            case TypeCode.Single:
                return (float)leftValue > (float)rightValue;
        }
        throw new NumericTypeExpectedException("Please compare valid numeric types.");
    }

    /// <summary>
    /// Checks if the value of left is the same as the value of right.
    /// </summary>
    public static bool operator ==(NumericType<T> left, NumericType<T> right)
    {
        return !(left > right) && !(left < right);
    }

    /// <summary>
    /// Checks if the value of left is not the same as the value of right.
    /// </summary>
    public static bool operator !=(NumericType<T> left, NumericType<T> right)
    {
        return !(left > right) || !(left < right);
    }

    /// <summary>
    /// Checks if left is either equal or smaller than right.
    /// </summary>
    public static bool operator <=(NumericType<T> left, NumericType<T> right)
    {
        return left == right || left < right;
    }

    /// <summary>
    /// Checks if left is either equal or greater than right.
    /// </summary>
    public static bool operator >=(NumericType<T> left, NumericType<T> right)
    {
        return left == right || left > right;
    }
}