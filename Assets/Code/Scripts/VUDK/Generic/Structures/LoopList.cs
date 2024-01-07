namespace VUDK.Generic.Structures
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class LoopList<T>
    {
        private int _index;

        [field: SerializeField]
        public List<T> List { get; private set; }

        public T Current
        {
            get
            {
                if (_index >= List.Count)
                    _index = 0;

                if (_index < 0)
                    _index = List.Count - 1;

                return List[_index];
            }
        }

        public LoopList(List<T> objects)
        {
            this.List = objects;
            _index = 0;
        }

        /// <summary>
        /// Resets the index.
        /// </summary>
        public void Reset()
        {
            _index = 0;
        }

        /// <summary>
        /// Gets the Current object and move the index to the next object.
        /// </summary>
        /// <returns>Current Object.</returns>
        public T Next()
        {
            T obj = Current;
            _index++;
            return obj;
        }

        /// <summary>
        /// Gets the Current object and move the index to the previous object.
        /// </summary>
        /// <returns>Current Object.</returns>
        public T Previous()
        {
            T obj = Current;
            _index--;
            return obj;
        }

        /// <summary>
        /// Checks if the List is empty.
        /// </summary>
        /// <returns>True if the List is empty, False if not.</returns>
        public bool IsEmpty()
        {
            return List.Count == 0;
        }

        /// <summary>
        /// Checks if the Current element is the last one in the list.
        /// </summary>
        /// <returns>True if the Current element is the last one, False if not.</returns>
        public bool IsLast()
        {
            return _index == List.Count - 1; 
        }

        /// <summary>
        /// Checks if the Current element is equals to a specific index.
        /// </summary>
        /// <returns>True if the Current element is equals to a specific index, False if not.</returns>
        public bool IsAt(int index)
        {
            return _index == index;
        }
    }
}
