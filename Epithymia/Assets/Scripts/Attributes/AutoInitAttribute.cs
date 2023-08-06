using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoInitAttribute : PropertyAttribute
    {
        public bool IsChildrenIncluded { get; }

        public AutoInitAttribute(bool isChildrenIncluded = false)
        {
            IsChildrenIncluded = isChildrenIncluded;
        }
    }
}
