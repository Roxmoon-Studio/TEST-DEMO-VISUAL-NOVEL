using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI.Characters
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] private CharacterSlot[] _slots;

        public async UniTask AddCharacter(CharacterVisualData visualData)
        {
            var handle = Addressables.LoadAssetAsync<Sprite>(visualData.CharacterAsset);
            await handle.Task;
            var sprite = handle.Result;
            var slot = _slots[(int) visualData.SlotPlace];
            
            await slot.SetCharacter(sprite);
        }

        public async UniTask SetCharacters(IEnumerable<CharacterVisualData> visualData)
        {
            await Clear();

            List<UniTask> tasks = new();

            foreach (var character in visualData)
                tasks.Add(AddCharacter(character));

            await UniTask.WhenAny(tasks);
        }


        public async UniTask MoveCharacter(CharacterTransitionData transitionData)
        {
            var movable = (int)transitionData.MovableSlot;
            var destination = (int)transitionData.DestinationSlot;
            await _slots[movable].MoveTo(_slots[destination]);
        }

        public async UniTask Clear()
        {
            List<UniTask> tasks = new();

            foreach (var slot in _slots)
                tasks.Add(slot.Clear());

            await UniTask.WhenAll(tasks);
        }

        public async UniTask ClearSlot(SlotPlace slotPlace)
        {
            CharacterSlot slot = _slots[(int)slotPlace];
            await slot.Clear();
        }
    }
}
