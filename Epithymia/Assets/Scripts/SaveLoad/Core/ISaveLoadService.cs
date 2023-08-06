namespace Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void Save<T>(T save, string name, SaveType saveType = SaveType.Game) where T : new();
        T Load<T>(string name, SaveType saveType = SaveType.Game) where T : new();
    }
}
