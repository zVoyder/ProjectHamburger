namespace VUDK.Generic.Utility
{
    using UnityEngine;

    public class GameObjectActivator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gameObject;

        /// <summary>
        /// Enables the GameObject.
        /// </summary>
        public void EnableGameObject()
        {
            _gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables the GameObject.
        /// </summary>
        public void DisableGameObject()
        {
            _gameObject.SetActive(false);
        }
    }
}