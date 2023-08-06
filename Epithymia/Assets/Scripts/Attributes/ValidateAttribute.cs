using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ValidateAttribute : PropertyAttribute
{
    public string Condition;
    public string Message;
        
    public ValidateAttribute(string condition, string message)
    {
        Condition = condition;
        Message = message;
    }
}