namespace VUDK.Extensions
{
    using System.Collections;
    using UnityEngine;

    public static class TransformExtension
    {
        /// <summary>
        /// Lerp rotates the transform so the forward vector points at target's current position.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target">Target transform.</param>
        /// <param name="t">Lerping value.</param>
        public static void LookAtLerp(this Transform self, Quaternion startRotation, Transform target, float t)
        {
            Vector3 relativePos = target.position - self.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            self.rotation = Quaternion.Lerp(startRotation, toRotation, t);
        }

        /// <summary>
        /// Checks if the path is clear between two Transforms.
        /// </summary>
        /// <param name="source">From.</param>
        /// <param name="target">To.</param>
        /// <param name="mask">Layer Mask.</param>
        /// <param name="maxDistance">Max considered distance.</param>
        /// <returns>True if it is clear, False if not.</returns>
        public static bool IsPathClear(this Transform source, Transform target, LayerMask mask, float maxDistance = Mathf.Infinity)
        {
            Vector3 direction = target.position - source.position;

            if (Physics.Raycast(source.position, direction, out RaycastHit hit, maxDistance, mask))
                return (hit.transform == target);

            return true;
        }

        /// <summary>
        /// Resets the Transform.
        /// </summary>
        public static void ResetTransform(this Transform transform, bool resetScale = true)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            if (resetScale)
                transform.localScale = Vector3.one;
        }

        /// <summary>
        /// Switches two transform positions with a lerp.
        /// </summary>
        /// <param name="transformA">From Transform.</param>
        /// <param name="transformB">To Transform.</param>
        /// <param name="duration">Duration in seconds.</param>
        /// <returns></returns>
        public static IEnumerator LerpSwitchPosition(this Transform transformA, Transform transformB, float duration)
        {
            Vector3 startPosA = transformA.position;
            Vector3 endPosA = transformB.position;

            Vector3 startPosB = transformB.position;
            Vector3 endPosB = transformA.position;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                transformA.position = Vector3.Lerp(startPosA, endPosA, t);
                transformB.position = Vector3.Lerp(startPosB, endPosB, t);
                yield return null;
            }
        }

        /// <summary>
        /// Sets the position of this transform.
        /// </summary>
        /// <param name="transform"><see cref="Transform"/> to set its position.</param>
        /// <param name="position">New position of the transform.</param>
        public static void SetPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }

        /// <summary>
        /// Sets the global scale of a transform by unparenting and re-parenting it.
        /// </summary>
        /// <param name="transform"><see cref="Transform"/> to set its scale.</param>
        /// <param name="scale">New scale of the transform.</param>
        public static void SetLossyScale(this Transform transform, Vector3 scale)
        {
            Transform oldParent = transform.parent;

            transform.parent = null;
            transform.localScale = scale;
            transform.parent = oldParent;
        }

        public static Vector3[] GetPositions(this Transform[] transforms)
        {
            Vector3[] positions = new Vector3[transforms.Length];
            for (int i = 0; i < transforms.Length; i++)
            {
                positions[i] = transforms[i].position;
            }
           return positions;
        }
    }
}