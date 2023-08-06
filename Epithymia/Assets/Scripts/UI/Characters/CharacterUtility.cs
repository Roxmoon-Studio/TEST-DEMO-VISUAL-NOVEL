using UnityEngine;

namespace UI.Characters
{
    public static class CharacterUtility
    {
        private const string KAT = "Кэт";

        public static bool IsKat(Sprite sprite) => 
            sprite.name.Contains(KAT);
    }
}
