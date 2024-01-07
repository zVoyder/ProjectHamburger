namespace VUDK.Features.Main.EntitySystem.Interfaces
{
    public interface IEntity : IVulnerable
    {
        /// <summary>
        /// Initializes this Entity.
        /// </summary>
        public void Init();

        /// <summary>
        /// Heals this Entity with heal points.
        /// </summary>
        /// <param name="healPoints">Heal points to heal with.</param>
        public void HealHitPoints(float healPoints);
    }
}