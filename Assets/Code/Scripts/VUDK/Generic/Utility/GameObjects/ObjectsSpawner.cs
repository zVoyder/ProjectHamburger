namespace VUDK.Generic.Utility
{
    using System.Collections;
    using UnityEngine;

    public abstract class ObjectsSpawner : MonoBehaviour
    {
        [SerializeField, Min(0), Header("Rate")]
        private float _spawnWaitRate = 1f;

        protected bool IsStarted { get; private set; }

        /// <summary>
        /// Begins the spawn.
        /// </summary>
        public virtual void StartSpawner()
        {
            IsStarted = true;
            StartCoroutine(SpawObjectsRoutine());
        }

        /// <summary>
        /// Stops the spawn coroutine.
        /// </summary>
        public void StopSpawn()
        {
            IsStarted = false;
            StopAllCoroutines();
        }

        /// <summary>
        /// Spawns the GameObject.
        /// </summary>
        /// <returns>Spawned GameObject.</returns>
        protected abstract void SpawnObject();

        /// <summary>
        /// Starts the spawn routine.
        /// </summary>
        private IEnumerator SpawObjectsRoutine()
        {
            while (true)
            {
                SpawnObject();
                yield return new WaitForSeconds(_spawnWaitRate);
            }
        }
    }
}