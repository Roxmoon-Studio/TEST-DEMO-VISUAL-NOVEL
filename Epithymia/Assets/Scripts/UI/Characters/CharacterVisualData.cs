using System;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Characters
{
    [Serializable]
    public struct CharacterVisualData
    {
        [field : SerializeField] public AssetReference CharacterAsset { get; private set; }
        [field : SerializeField] public SlotPlace SlotPlace { get; private set; }
    }
}
