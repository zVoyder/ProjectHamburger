namespace VUDK.Generic.Utility.GameObjects
{
    using System.Collections;
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class GameobjectEnablerLoop : MonoBehaviour
    {
        [SerializeField, Header("Target Gameobject")]
        private GameObject _gameObject;

        [SerializeField, Header("Cooldown")]
        private Range<float> _cooldown;

        [SerializeField]
        private bool _startOnAwake;

        private bool _isLooping;

        protected virtual void Awake()
        {
            if(_startOnAwake)
                StartCoroutine(EnableRoutine(true));
        }

        public void StartLoop()
        {
            if (_isLooping) return;

            _isLooping = true;
            StartCoroutine(EnableRoutine(true));
        }

        public void Stop()
        {
            StopAllCoroutines();
        }

        private IEnumerator EnableRoutine(bool enabled)
        {
            _gameObject.SetActive(enabled);
            yield return new WaitForSeconds(_cooldown.Random());
            StartCoroutine(EnableRoutine(!enabled));
        }
    }
}