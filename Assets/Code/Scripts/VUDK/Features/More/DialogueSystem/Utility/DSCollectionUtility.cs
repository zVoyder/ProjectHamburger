namespace VUDK.Features.More.DialogueSystem.Utility
{
    using System.Collections.Generic;
    using VUDK.Generic.Serializable;

    public static class DSCollectionUtility
    {
        public static void AddItem<T, TKey>(this SerializableDictionary<T, List<TKey>> serializableDictionary, T key, TKey value)
        {
            if (serializableDictionary.ContainsKey(key))
            {
                serializableDictionary[key].Add(value);
                return;
            }

            serializableDictionary.Add(key, new List<TKey>() { value });
        }
    }
}