namespace VUDK.Generic.Serializable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SerializableDictionary
    {
    }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary, IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        public struct SerializableKeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public SerializableKeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public void SetValue(TValue value)
            {
                Value = value;
            }
        }

        [SerializeField]
        private List<SerializableKeyValuePair> _dictionary = new List<SerializableKeyValuePair>();

        private Dictionary<TKey, uint> KeyPositions => _keyPositions.Value;
        private Lazy<Dictionary<TKey, uint>> _keyPositions;

        public SerializableDictionary()
        {
            _keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
        }

        public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);

            if (dictionary == null)
                throw new ArgumentException("The passed dictionary is null.");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                Add(pair.Key, pair.Value);
        }

        private Dictionary<TKey, uint> MakeKeyPositions()
        {
            int numEntries = _dictionary.Count;
            Dictionary<TKey, uint> result = new Dictionary<TKey, uint>(numEntries);
            for (int i = 0; i < numEntries; ++i)
                result[_dictionary[i].Key] = (uint)i;

            return result;
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            // After deserialization, the key positions might be changed
            _keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
        }

        #region IDictionary
        public TValue this[TKey key]
        {
            get => _dictionary[(int)KeyPositions[key]].Value;
            set
            {
                if (KeyPositions.TryGetValue(key, out uint index))
                {
                    _dictionary[(int)index].SetValue(value);
                }
                else
                {
                    KeyPositions[key] = (uint)_dictionary.Count;

                    _dictionary.Add(new SerializableKeyValuePair(key, value));
                }
            }
        }

        public ICollection<TKey> Keys => _dictionary.Select(tuple => tuple.Key).ToArray();

        public ICollection<TValue> Values => _dictionary.Select(tuple => tuple.Value).ToArray();

        public bool ContainsKey(TKey key) => KeyPositions.ContainsKey(key);

        public void Add(TKey key, TValue value)
        {
            if (KeyPositions.ContainsKey(key))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            }
            else
            {
                KeyPositions[key] = (uint)_dictionary.Count;
                _dictionary.Add(new SerializableKeyValuePair(key, value));
            }
        }

        public bool Remove(TKey key)
        {
            if (KeyPositions.TryGetValue(key, out uint index))
            {
                Dictionary<TKey, uint> kp = KeyPositions;
                kp.Remove(key);
                _dictionary.RemoveAt((int)index);

                int numEntries = _dictionary.Count;
                for (uint i = index; i < numEntries; i++)
                    kp[_dictionary[(int)i].Key] = i;

                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (KeyPositions.TryGetValue(key, out uint index))
            {
                value = _dictionary[(int)index].Value;
                return true;
            }

            value = default;
            return false;
        }
        #endregion

        #region ICollection
        public int Count => _dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(KeyValuePair<TKey, TValue> kvp) => Add(kvp.Key, kvp.Value);

        public void Clear()
        {
            _dictionary.Clear();
            KeyPositions.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> kvp) => KeyPositions.ContainsKey(kvp.Key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int numKeys = _dictionary.Count;

            if (array.Length - arrayIndex < numKeys)
            {
                throw new ArgumentException("arrayIndex");
            }

            for (int i = 0; i < numKeys; ++i, ++arrayIndex)
            {
                SerializableKeyValuePair entry = _dictionary[i];

                array[arrayIndex] = new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> kvp) => Remove(kvp.Key);
        #endregion

        #region IEnumerable
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.Select(ToKeyValuePair).GetEnumerator();

            KeyValuePair<TKey, TValue> ToKeyValuePair(SerializableKeyValuePair skvp)
            {
                return new KeyValuePair<TKey, TValue>(skvp.Key, skvp.Value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}