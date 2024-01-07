namespace VUDK.Generic.Serializable
{
    using System;
    using UnityEngine;

    [System.Serializable]
    public class Range<T> where T : struct, IComparable<T>
    {
        [field: SerializeField]
        public T Min { get; set; }

        [field: SerializeField]
        public T Max { get; set; }

        public Range(T min, T max)
        {
            Set(min, max);
        }

        /// <summary>
        /// Sets the minimum and maximum numbers.
        /// </summary>
        /// <param name="min">Inclusive minimum number.</param>
        /// <param name="max">Inclusive maximum number.</param>
        public void Set(T min, T max)
        {
            Max = max.CompareTo(min) > 0 ? max : min;
            Min = min.CompareTo(max) < 0 ? min : max;
        }

        ///// <summary>
        ///// Gets a random T number.
        ///// </summary>
        ///// <returns>Random number of type T.</returns>
        public T Random()
        {
            float randomValue = UnityEngine.Random.Range(Convert.ToSingle(Min), Convert.ToSingle(Max));
            return (T)Convert.ChangeType(randomValue, typeof(T));
        }
    }
}