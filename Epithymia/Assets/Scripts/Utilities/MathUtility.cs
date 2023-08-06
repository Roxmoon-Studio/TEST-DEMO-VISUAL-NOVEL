using UnityEngine;

namespace Utilities
{
    public static class MathUtility
    {
        public const float EPSILON = 0.00001f;
        
        public static bool Approximately(float a, float b, float epsilon = EPSILON) => 
            Mathf.Abs(a - b) < EPSILON;
    }
}
