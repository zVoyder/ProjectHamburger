namespace VUDK.Features.Main.CheckpointSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.EntitySystem.Interfaces;

    public static class CheckpointsManager
    {
        private static Dictionary<IEntity, Vector3> s_checkpoints = new Dictionary<IEntity, Vector3>();

        public static void SetCheckpoint(IEntity entity, Vector3 position)
        {
            if (!s_checkpoints.ContainsKey(entity))
                s_checkpoints.Add(entity, position);

            s_checkpoints[entity] = position;
        }

        public static bool TryGetLastCheckpoint(IEntity requestingEntity, out Vector3 position)
        {
            if (!s_checkpoints.ContainsKey(requestingEntity))
            {
                position = Vector3.zero;
                return false;
            }

            position = s_checkpoints[requestingEntity];
            return true;
        }

        public static void RemoveCheckpoint(IEntity entity)
        {
            if (!s_checkpoints.ContainsKey(entity))
                return;

            s_checkpoints.Remove(entity);
        }
    }
}