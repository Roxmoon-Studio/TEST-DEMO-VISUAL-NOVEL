using System;
using Enums;
using UnityEngine;

namespace UI.Characters
{
    [Serializable]
    public struct CharacterTransitionData
    {
        [field: SerializeField] public SlotPlace MovableSlot { get; private set; }
        [field: SerializeField] public SlotPlace DestinationSlot { get; private set; }
        [field: SerializeField] public CharacterTransitionType CharacterTransitionType { get; private set; }
    }
}
