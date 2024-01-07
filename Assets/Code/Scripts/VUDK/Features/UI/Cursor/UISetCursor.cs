namespace VUDK.Features.UI.Cursor
{
    using UnityEngine;

    public class UISetCursor : MonoBehaviour
    {
        [SerializeField, Header("Cursor Visibility")]
        private bool _isEnableOnAwake;

        private void Awake()
        {
            Cursor.visible = _isEnableOnAwake;
        }
    }
}
