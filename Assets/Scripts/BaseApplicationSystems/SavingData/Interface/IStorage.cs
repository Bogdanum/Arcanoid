public interface IStorage
{
    void Save(GameData gameData);
    GameData Load();
}
