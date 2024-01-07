namespace VUDK.Features.Main.SaveSystem.Interfaces
{
    using VUDK.Features.Main.SaveSystem.SaveData;

    public interface ISavable : IPull, IPush
    {
        public int SaveID { get; }
        public string GetSaveName();
    }
}