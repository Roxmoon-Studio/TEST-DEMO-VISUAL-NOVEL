using System;
using UnityEngine;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GUIColorAttribute : PropertyAttribute
    {
        public Color Color { get; }

        public GUIColorAttribute(float r, float g, float b, float a = 1f)
        {
            Color = new Color(r, g, b, a);
        }
    }
}
