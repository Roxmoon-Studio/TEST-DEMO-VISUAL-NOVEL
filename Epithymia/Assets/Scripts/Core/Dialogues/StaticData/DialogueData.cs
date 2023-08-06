using System;
using UnityEngine;

namespace Core.Dialogues.StaticData
{
    [Serializable]
    public class DialogueData
    {
        [field : SerializeField] public string Author { get; private set; }
        [field : SerializeField, TextArea(10, 100)] public string Text { get; private set; }
        [field : SerializeField] public bool IsFaded { get; private set; }
    }
}
