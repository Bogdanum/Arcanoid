public interface IGameDataStorage
{
    void Save(GameData gameData);
    GameData Load();
}
