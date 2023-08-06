using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Characters
{
    public class GameObjectPool<T> where T : Component
    {
        public int Size { get; private set; }
        public int FreeSize => _freeObjects.Count;

        private readonly T _prefab;
        private readonly Stack<T> _freeObjects = new();
        private readonly HashSet<T> _objectsRegister = new();
        private readonly bool _allowScaling;

        public GameObjectPool(T prefab, int size = 0, bool allowScaling = true)
        {
            _prefab = prefab;
            _allowScaling = allowScaling;

            InstantiatePool(size);
        }

        public T Get()
        {
            if (_freeObjects.TryPop(out T result))
            {
                result.gameObject.SetActive(true);

                return result;
            }

            if (!_allowScaling)
                throw new OperationCanceledException("Pool is empty. Allow Scaling == false!");

            Instantiate();

            result = _freeObjects.Pop();
            result.gameObject.SetActive(true);

            return _freeObjects.Pop();
        }

        public void Release(T subject)
        {
            if (!_objectsRegister.Contains(subject))
                throw new OperationCanceledException("This object does not belong to this pool!");

            subject.gameObject.SetActive(false);
            _freeObjects.Push(subject);
        }

        private void InstantiatePool(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Instantiate();
            }
        }

        private void Instantiate()
        {
            var go = Object.Instantiate(_prefab);
            _freeObjects.Push(go);
            _objectsRegister.Add(go);

            Size++;
        }

    }
}
