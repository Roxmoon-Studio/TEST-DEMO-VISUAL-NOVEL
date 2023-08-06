using System.Collections.Generic;
using Attributes;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextWhisper : MonoBehaviour
    {
        [SerializeField, AutoInit] private TextMeshProUGUI _textMesh;

        private readonly List<string> _randomPhrases = new()
        {
            "Random Phrase 1",
            "Random Phrase 2",
            "Random Phrase 3",
            "Random Phrase 4",
            "Random Phrase 5",
            "Random Phrase 6",
            "Random Phrase 7",
        };

        private void Awake()
        {
            _textMesh.text = GetRandomPhrase();
        }

        private string GetRandomPhrase()
        {
            var index = Random.Range(0, _randomPhrases.Count);
            
            return _randomPhrases[index];
        }
    }
}
