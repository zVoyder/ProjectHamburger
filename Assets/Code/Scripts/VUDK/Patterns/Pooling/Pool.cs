namespace VUDK.Patterns.Pooling
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Patterns.Pooling.Interfaces;

    public sealed class Pool : MonoBehaviour
    {
        [Tooltip("The prefab to be pooled.")]
        [SerializeField]
        private GameObject _pooledObject;

        [Tooltip("The initial number of instances to create in the pool.")]
        [SerializeField]
        [Min(0)]
        private int _instancesAtStart = 1;

        [Tooltip("Whether the pool has a maximum capacity.")]
        [SerializeField]
        private bool _isCapped = false;

        private Queue<GameObject> _instances;

        private void Awake()
        {
            _instances = new Queue<GameObject>();
        }

        private void Start()
        {
            Instantiate(_instancesAtStart);
        }

        /// <summary>
        /// Gets a GameObject from the pool list.
        /// </summary>
        /// <returns>GameObject from the pool.</returns>
        public GameObject Get()
        {
            if (IsEmpty())
            {
                if (_isCapped)
                    return null;

                Instantiate();
            }

            GameObject deq = _instances.Dequeue();
            deq.SetActive(true);

            return deq;
        }

        public bool TryGet(out GameObject pooledObject)
        {
            pooledObject = Get();

            if(!pooledObject)
                return false;

            return true;
        }

        /// <summary>
        /// Gets a GameObject from the pool list and changes its parent.
        /// </summary>
        /// <param name="parent">The new GameObject transform parent.</param>
        /// <param name="resetTransform">True to reset its transform, False to not reset its transform.</param>
        /// <returns>GameObject from the pool.</returns>
        public GameObject Get(Transform parent, bool resetTransform = true)
        {
            GameObject deq = Get();
            deq.transform.SetParent(parent);

            if (resetTransform)
                deq.transform.ResetTransform();

            return deq;
        }

        /// <summary>
        /// Disposes a GameObject and returns it to the pool list.
        /// </summary>
        /// <param name="pooledObject">Object to Pool.</param>
        public void Dispose(GameObject pooledObject)
        {
            Clear(pooledObject);
            _instances.Enqueue(pooledObject);
        }

        private void Clear(GameObject pooledObject)
        {
            GetPooledObject(pooledObject).Clear();
            pooledObject.transform.SetParent(transform, true);
            //pooledObject.transform.ResetTransform(false);
            pooledObject.SetActive(false);
        }

        private void Instantiate(int quantity)
        {
            for (int i = 0; i < quantity; i++)
                Instantiate();
        }

        private void Instantiate()
        {
            if (!IsPooledObject(_pooledObject))
                return;

            GameObject createdPooledObject = Instantiate(_pooledObject);
            GetPooledObject(createdPooledObject).AssociatePool(this);

            Dispose(createdPooledObject);
        }

        private IPooledObject GetPooledObject(GameObject pooledGameobject)
        {
            if (!IsPooledObject(pooledGameobject))
            {
                return null;
            }

            pooledGameobject.TryGetComponent(out IPooledObject pooledObject);
            return pooledObject;
        }

        private bool IsPooledObject(GameObject pooledGameobject)
        {
            if (!pooledGameobject.TryGetComponent(out IPooledObject pooledObjectCheck))
            {
#if UNITY_EDITOR
                Debug.LogError($"GameObject {_pooledObject.transform.name} is not a IPooledObject.");
#endif
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the pool list is empty.
        /// </summary>
        /// <returns>True if it is empty, False if not.</returns>
        private bool IsEmpty()
        {
            return _instances.Count == 0;
        }
    }
}