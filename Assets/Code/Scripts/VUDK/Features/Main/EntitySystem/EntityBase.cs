namespace VUDK.Features.Main.EntitySystem
{
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EntitySystem.Interfaces;
    using VUDK.Features.Main.EventSystem;

    public abstract class EntityBase : MonoBehaviour, IEntity
    {
        [field: SerializeField, Min(0)]
        public float StartingHitPoints { get; protected set; }

        [field: SerializeField, Min(0)]
        public float MaxHitPoints { get; protected set; }

        [field: SerializeField]
        public bool IsInvulnerable { get; protected set; }

        public float CurrentHitPoints { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public void Init()
        {
            IsAlive = true;
            CurrentHitPoints = StartingHitPoints;

            if (CurrentHitPoints > StartingHitPoints)
            {
                StartingHitPoints = MaxHitPoints;
                CurrentHitPoints = StartingHitPoints;
            }

            EventManager.Ins.TriggerEvent(EventKeys.EntityEvents.OnEntityInit, this);
        }

        public void TakeDamage(float hitDamage = 1f)
        {
            if (IsInvulnerable)
                hitDamage = 0f;

            OnTakeDamage(hitDamage);
            CurrentHitPoints -= Mathf.Abs(hitDamage);

            if (CurrentHitPoints <= 0.1f)
            {
                CurrentHitPoints = 0f;
                Death();
            }

            EventManager.Ins.TriggerEvent(EventKeys.EntityEvents.OnEntityTakeDamage, this);
        }

        public void HealHitPoints(float healPoints)
        {
            OnHeal(healPoints);

            IsAlive = true;
            CurrentHitPoints += Mathf.Abs(healPoints);

            if (CurrentHitPoints > MaxHitPoints)
                CurrentHitPoints = MaxHitPoints;

            EventManager.Ins.TriggerEvent(EventKeys.EntityEvents.OnEntityHeal, this);
        }

        public void Death()
        {
            if (IsAlive)
            {
                IsAlive = false;
                OnDeath();
                EventManager.Ins.TriggerEvent(EventKeys.EntityEvents.OnEntityDeath, this);
            }
        }

        /// <summary>
        /// Called when this entity dies.
        /// </summary>
        protected virtual void OnDeath()
        {
        }

        /// <summary>
        /// Called when this entity takes damage.
        /// </summary>
        /// <param name="damageTaken">Hit points that has been damaged.</param>
        protected virtual void OnTakeDamage(float damageTaken)
        {
        }

        /// <summary>
        /// Called when this entity heals.
        /// </summary>
        /// <param name="healTaken">Hit points that has been healed.</param>
        protected virtual void OnHeal(float healTaken)
        {
        }
    }
}