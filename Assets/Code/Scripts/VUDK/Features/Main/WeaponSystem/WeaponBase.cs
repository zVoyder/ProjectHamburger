namespace VUDK.Features.Main.WeaponSystem
{
    using System;
    using System.Collections;
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public abstract class WeaponBase : MonoBehaviour
    {
        [Header("Damage")]
        public Range<float> Damage;

        public float FireRate = 0.2f;

        [Header("Ammo")]
        public bool HasInfiniteAmmo = false;

        [SerializeField]
        protected float MaxAmmunition;

        [SerializeField]
        protected float StartingAmmunition;

        [SerializeField]
        protected float AmmunitionCostPerShot;

        [SerializeField, Header("Barrels")]
        protected Transform[] BarrelsPoints;

        protected float CurrentAmmunition;

        public bool IsShooting { get; protected set; }

        protected bool HasAmmo => (CurrentAmmunition - AmmunitionCostPerShot >= 0f) || HasInfiniteAmmo;

        public event Action OnShoot;
        public event Action OnAmmoFinished;

        /// <summary>
        /// Initializes this <see cref="WeaponBase"/>.
        /// </summary>
        public virtual void Init()
        {
            CurrentAmmunition = StartingAmmunition;

            if (StartingAmmunition > MaxAmmunition)
            {
                StartingAmmunition = MaxAmmunition;
                CurrentAmmunition = StartingAmmunition;
            }
        }

        /// <summary>
        /// Shoots with this <see cref="WeaponBase"/>.
        /// </summary>
        public virtual void PullTrigger()
        {
            if (HasAmmo && !IsShooting)
            {
                OnShoot?.Invoke();
                StartCoroutine(ShootingRoutine());
            }
        }

        /// <summary>
        /// Adds Ammunition.
        /// </summary>
        /// <param name="ammoToAdd">Ammo quantity to add.</param>
        public virtual void AddAmmunition(float ammoToAdd)
        {
            CurrentAmmunition += ammoToAdd;

            if (CurrentAmmunition > MaxAmmunition)
                CurrentAmmunition = MaxAmmunition;
        }

        /// <summary>
        /// On Generation of the bullet.
        /// </summary>
        protected virtual void OnBulletGeneration()
        {
        }

        /// <summary>
        /// Shooting Coroutine.
        /// </summary>
        private IEnumerator ShootingRoutine()
        {
            OnBulletGeneration();
            CurrentAmmunition -= AmmunitionCostPerShot;
            IsShooting = true;
            yield return new WaitForSeconds(FireRate);
            IsShooting = false;

            if (!HasAmmo)
                OnAmmoFinished?.Invoke();
        }
    }
}