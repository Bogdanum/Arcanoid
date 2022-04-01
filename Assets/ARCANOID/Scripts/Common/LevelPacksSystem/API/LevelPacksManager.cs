using System.Collections.Generic;
using UnityEngine;

public class LevelPacksManager : MonoBehaviour
{
    private CurrentSetOfPacksConfig _config;
    private GameProgressProcessor _progressProcessor;
    private ProgressSaveProvider _saveProvider;
    private ILevelParser<LevelData<TileProperties>> _levelParser;
#if  UNITY_EDITOR
    private bool _needDebugInit = true;
#endif

    public void Init(JsonTokens jsonTokens, TextAsset tilesetFile, CurrentSetOfPacksConfig config, StoredDataManager storedDataManager)
    {
        _config = config;
        var packs = _config.GetPacks();
        _saveProvider = new ProgressSaveProvider(storedDataManager, packs);
        _progressProcessor = new GameProgressProcessor(_saveProvider, packs);
        _levelParser = new JsonLevelParser(tilesetFile.text, jsonTokens);
    }

    public LevelData<TileProperties> GetCurrentLevelData()
    {
        #if UNITY_EDITOR

            if (_config.gameMode == CurrentSetOfPacksConfig.GameMode.Debug && _needDebugInit)
            {
                SetupFirstPack();
                MessageBus.RaiseEvent<IPackActionHandler>(handler => handler.OnChoosingAnotherPack());
                _needDebugInit = false;
            }

        #endif
        
        var level = _progressProcessor.GetCurrentLevel();
       return _levelParser.ParseLevelFromString(level.text);
    }

    public bool IsFirsTimePlay() => !_saveProvider.IsSaveExistsOnStart;

    public void SetupFirstPack() => _progressProcessor.StartLevelPack(_config.StartPackID);

    public void SetCurrentPack(string packId) => _progressProcessor.StartLevelPack(packId);
    
    public LevelPackInfo GetCurrentPackInfo() => _progressProcessor.GetCurrentPackInfo();

    public Dictionary<string, LevelPackInfo> GetPackInfos() => _progressProcessor.GetPackInfos();

    public void OnLevelComplete()
    {
        _progressProcessor.OnLevelComplete();
        _saveProvider.SaveProgress();
    }
}
