namespace VUDK.Features.Main.InputSystem.MobileInputs.Utility
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public static class MobileInputsUtility
    {
        /// <summary>
        /// Returns the position of the touch in the screen already converted with ScreenToWorldPoint.
        /// </summary>
        /// <param name="camera">Camera to convert position from, if null select <see cref="Camera.main"/>.</param>
        public static Vector2 GetScreenTouchPosition(Camera camera = null)
        {
            if (camera == null)
                camera = MainManager.Ins.GameStats.MainCamera;

            Vector3 touchPos = GetRawTouchPosition();
            return camera.ScreenToWorldPoint(touchPos);
        }

        /// <summary>
        /// Returns the position of the touch in the screen not converted with <see cref="Camera.ScreenPointToRay(Vector3)"/>.
        /// </summary>
        /// <returns>Raw position of the touch in the screen.</returns>
        public static Vector2 GetRawTouchPosition()
        {
            return InputsManager.Inputs.Touches.PrimaryTouchPosition.ReadValue<Vector2>();
        }

        /// <summary>
        /// Raycast2D from the finger touch position to the world.
        /// </summary>
        /// <param name="layerMask">Layers to ingnore.</param>
        /// <returns><see cref="RaycastHit2D"/> of the hit.</returns>
        public static RaycastHit2D RaycastFromTouch2D(float maxDistance = 10f, LayerMask layerMask = default)
        {
            Vector2 origin = GetScreenTouchPosition();
            Vector2 direction = Vector2.zero;
            return Physics2D.Raycast(origin, direction, maxDistance, layerMask);
        }

        /// <summary>
        /// Checks with <see cref="RaycastFromTouch2D(LayerMask)"/> if the touch is on a T Component.
        /// </summary>
        /// <typeparam name="T">T Component to check.</typeparam>
        /// <param name="component">Found T component.</param>
        /// <param name="layerMask">Layers to ingnore.</param>
        /// <returns>True if the T component is found, False if not.</returns>
        public static bool IsTouchOn2D<T>(out T component, LayerMask layerMask = default) where T : Component
        {
            RaycastHit2D hit = RaycastFromTouch2D(layerMask);
            if (hit)
                return hit.transform.TryGetComponent(out component);

            component = null;
            return false;
        }

        public static bool RaycastFromTouch(out RaycastHit hit, float maxDistance = 10f, LayerMask layerMask = default)
        {
            Vector3 touchPosition = GetRawTouchPosition();
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            return Physics.Raycast(ray, out hit, maxDistance, layerMask);
        }

        public static bool IsTouchOn<T>(out T component, float maxDistance = 10f, LayerMask layerMask = default) where T : Component
        {
            if (RaycastFromTouch(out RaycastHit hit, maxDistance, layerMask))
                return hit.transform.TryGetComponent(out component);

            component = null;
            return false;
        }
    }
}
