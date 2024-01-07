namespace VUDK.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class CollectionExtension
    {
        public static KeyValuePair<TKey, TValue> GetRandomElementAndRemove<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            int randomIndex = Random.Range(0, dictionary.Count);
            KeyValuePair<TKey, TValue> randomElement = dictionary.ElementAt(randomIndex);
            dictionary.Remove(randomElement.Key);
            return randomElement;
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}