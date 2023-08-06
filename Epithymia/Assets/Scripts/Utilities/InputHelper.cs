using UnityEngine;

namespace Utilities
{
    public static class InputHelper
    {
        public static bool CheckSkipDialogue() => 
            IsKeyDown(KeyCode.Space, KeyCode.Return);

        public static bool CheckSkipIntro() => 
            IsKeyDown(KeyCode.Escape, KeyCode.Space, KeyCode.Return);

        public static bool IsKeyDown(params KeyCode[] keyCodes)
        {
            foreach (var keyCode in keyCodes)
                if (Input.GetKeyDown(keyCode))
                    return true;

            return false;
        }
    }
}
