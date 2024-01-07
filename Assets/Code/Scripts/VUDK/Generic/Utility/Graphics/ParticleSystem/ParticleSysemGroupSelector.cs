namespace VUDK.Generic.Utility.Graphics.ParticleSystem
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class ParticleSystemGroupSelector : MonoBehaviour
    {
        [Header("Group")]
        [SerializeField]
        private bool _shouldPlayIndividually = false;
        [SerializeField]
        private SerializableDictionary<string, ParticleSystem> _particles = new SerializableDictionary<string, ParticleSystem>();

        public void Play(string particleKey)
        {
            if (!_particles.ContainsKey(particleKey)) return;

            if (_shouldPlayIndividually)
                Stop();

            _particles[particleKey].Play();
        }

        public void Stop()
        {
            foreach (var particle in _particles.Values)
                particle.Stop();
        }
    }
}
