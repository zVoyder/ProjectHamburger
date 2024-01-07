namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;

    [DefaultExecutionOrder(-895)]
    public abstract class UIManagerBase : MonoBehaviour
    {
        [SerializeField, Header("UI Canvas")]
        protected Canvas _canvas;
    }
}