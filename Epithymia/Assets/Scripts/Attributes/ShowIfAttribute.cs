using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ShowIfAttribute : PropertyAttribute
{
    public string Condition;

    public ShowIfAttribute(string condition)
    {
        Condition = condition;
    }
}
