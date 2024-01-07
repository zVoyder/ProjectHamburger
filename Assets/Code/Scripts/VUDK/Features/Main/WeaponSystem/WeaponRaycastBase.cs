namespace VUDK.Features.Main.WeaponSystem
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.EntitySystem.Interfaces;

    public class WeaponRaycastBase : WeaponBase
    {
        [SerializeField, Header("Raycast")]
        protected float RaycastShootRange;
        [SerializeField]
        private LayerMask _rayShootableMask;

        public static event Action<Vector3> OnBulletHit;

        protected override void OnBulletGeneration()
        {
            base.OnBulletGeneration();
            foreach (Transform barrel in BarrelsPoints)
            {
#if UNITY_EDITOR
                Debug.DrawRay(barrel.position, barrel.forward * RaycastShootRange);
#endif
                if (Physics.Raycast(barrel.position, barrel.forward, out RaycastHit hit, RaycastShootRange, _rayShootableMask))
                {
                    OnBulletHit?.Invoke(hit.point);

                    if (hit.transform.TryGetComponent(out IVulnerable ent))
                        ent.TakeDamage(Damage.Random());
                }
            }
        }
    }
}