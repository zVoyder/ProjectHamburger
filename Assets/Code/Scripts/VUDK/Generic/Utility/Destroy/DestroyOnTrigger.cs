namespace VUDK.Generic.Utility.Destroy
{
    using UnityEngine;

    public class DestroyOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private string _tagName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tagName))
            {
                Destroy(gameObject);
            }
        }
    }
}