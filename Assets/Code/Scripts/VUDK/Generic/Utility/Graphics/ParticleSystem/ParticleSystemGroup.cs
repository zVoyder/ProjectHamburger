namespace VUDK.Generic.Utility.Graphics.ParticleSystem
{
    using UnityEngine;

    public class ParticleSystemGroup : MonoBehaviour
    {
        [SerializeField, Header("Group")]
        private ParticleSystem[] _particles;

        public void Play()
        {
            foreach (var particle in _particles)
                particle.Play();
        }

        public void Stop()
        {
            foreach(var particle in _particles)
                particle.Stop();
        }
    }
}
