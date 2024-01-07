namespace VUDK.Features.Main.SaveSystem.Bases
{
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.SaveData;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class SaverBase<T> : MonoBehaviour, ISavable, IInit where T : SaveValue, new ()
    {
        protected T SaveValue;

        public int SaveID => GetInstanceID();

        protected virtual void Awake()
        {
            Pull();
            Init();
        }

        public abstract void Init();

        public bool Check()
        {
            return SaveValue != null;
        }

        public void Push()
        {
            SavePacketData saveData = new SavePacketData(SaveValue);
            SaveManager.Push(GetSaveName(), SaveID, saveData);
        }

        public void Pull()
        {
            if (SaveManager.TryPull<T>(GetSaveName(), SaveID, out SavePacketData _saveData))
                SaveValue = _saveData.Value as T;
            else
                SaveValue = new T();
        }

        public virtual string GetSaveName()
        {
            return GetType().Name;
        }
    }
}