namespace VUDK.Patterns.Pooling
{
    using UnityEngine;
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Serializable;

    [DefaultExecutionOrder(-100)]
    public sealed class PoolsManager : MonoBehaviour
    {
        [field: SerializeField]
        public SerializableDictionary<ScriptableKey, Pool> Pools { get; private set; }
    }
}