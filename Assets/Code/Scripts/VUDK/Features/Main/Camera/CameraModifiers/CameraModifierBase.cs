namespace VUDK.Features.Main.Camera.CameraModifiers
{
    using UnityEngine;

    [RequireComponent(typeof(Camera))]
    public abstract class CameraModifierBase : MonoBehaviour
    {
        protected Camera Camera { get; private set; }

        protected virtual void Awake()
        {
            TryGetComponent(out Camera _camera);
            Camera = _camera;
        }
    }
}
