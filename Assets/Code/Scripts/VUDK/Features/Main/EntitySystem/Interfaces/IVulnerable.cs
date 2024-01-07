namespace VUDK.Features.Main.EntitySystem.Interfaces
{
    public interface IVulnerable
    {
        /// <summary>
        /// Takes damage.
        /// </summary>
        /// <param name="hitDamage">Damages to take.</param>
        void TakeDamage(float hitDamage = 1f);

        /// <summary>
        /// Dies.
        /// </summary>
        void Death();
    }
}