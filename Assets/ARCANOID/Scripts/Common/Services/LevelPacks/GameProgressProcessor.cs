using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

public class GameProgressProcessor
{
    private readonly ProgressSaveProvider _progressSaveProvider;
    private readonly Dictionary<string, LevelPack> _packs;
    private Dictionary<string, LevelPackInfo> _packInfos;

    private string _currentPackID;
    private int _currentLevel;

    public GameProgressProcessor(ProgressSaveProvider progressSaveProvider, Dictionary<string, LevelPack> packs)
    {
        _progressSaveProvider = progressSaveProvider;
        _packs = packs;
        InitLevelPackInfos();
    }

    private void InitLevelPackInfos()
    {
        _packInfos = new Dictionary<string, LevelPackInfo>();
        foreach (var id in _packs.Keys)
        {
            _packInfos.Add(id, new LevelPackInfo());
            RefreshPackInfo(id);
        }
    }

    private void RefreshPackInfo(string packID)
    {
        var packInfo = _packInfos[packID];
        packInfo.Pack = _packs[packID];

        if (_progressSaveProvider.StoredProgressContainsKey(packID))
        {
            bool completeStatus = _progressSaveProvider.IsPackComplete(packID);
            int currentLevel = _progressSaveProvider.GetCurrentLevel(packID);
            int levelsCount = _packs[packID].Count;
            packInfo.CurrentLevel = currentLevel + 1;
            packInfo.CompletedLevels = completeStatus ? levelsCount : currentLevel;
            packInfo.IsOpened = _progressSaveProvider.IsPackOpen(packID);
        }
    }

    public TextAsset GetCurrentLevel()
    {
        return _packs[_currentPackID].GetLevel(_currentLevel);
    }
    
    public LevelPackInfo GetCurrentPackInfo()
    {
        return _currentPackID == null ? null : _packInfos[_currentPackID];
    }
    
    public Dictionary<string, LevelPackInfo> GetPackInfos() => _packInfos;

    public void StartLevelPack(string packID)
    {
        _currentPackID = packID;
        _currentLevel = _progressSaveProvider.GetCurrentLevel(packID);
    }
    
    public void OnLevelComplete()
    {
        _currentLevel++;
        if (_currentLevel < _packs[_currentPackID].Count)
        {
            NextLevel();
        }
        else
        {
            NextPack();
        }
        RefreshPackInfo(_currentPackID);
    }

    private void NextLevel()
    {
        _progressSaveProvider.SetCurrentLevel(_currentPackID, _currentLevel);
        _packInfos[_currentPackID].IsRepassed = false;
    }
    
    private void NextPack()
    {
        _currentLevel = 0;
        _progressSaveProvider.SetCurrentLevel(_currentPackID, _currentLevel);
        if (!_progressSaveProvider.IsPackComplete(_currentPackID))
        {
            _progressSaveProvider.CompletePack(_currentPackID);
            RefreshPackInfo(_currentPackID);
            OpenNextPack();
        }
        else
        {
            _packInfos[_currentPackID].IsRepassed = true;
        }
    }

    private void OpenNextPack()
    {
        string[] packIds = _packs.Keys.ToArray();
        int packIdIndex = packIds.IndexOf(_currentPackID);
        int currentID = packIdIndex + 1;

        if (currentID < packIds.Length)
        {
            _currentPackID = packIds[currentID];
            _progressSaveProvider.OpenPack(_currentPackID);
            return;
        }
        _packInfos[_currentPackID].IsLast = true;
    }
    
}
