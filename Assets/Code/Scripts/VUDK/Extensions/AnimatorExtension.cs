namespace VUDK.Extensions
{
    using UnityEngine;

    public static class AnimatorExtension
    {
        /// <summary>
        /// Refreshes the animator to update the transforms is interacting with.
        /// </summary>
        public static void RefreshAnimator(this Animator anim)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            anim.updateMode = AnimatorUpdateMode.Normal;
        }
    }
}