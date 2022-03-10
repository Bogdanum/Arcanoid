using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JsonParserConfig", menuName = "Localization/JsonParserConfig")]
public class JsonParserConfig : LanguageParserConfig
{
    [SerializeField] private TranslationsFiles[] _translationsFiles;

    private Dictionary<LanguagesEnums.Language, TextAsset> _translationsFileDictionary;

    public void Init()
    {
        _translationsFileDictionary = new Dictionary<LanguagesEnums.Language, TextAsset>();
        foreach (var file in _translationsFiles)
        {
            _translationsFileDictionary.Add(file.language, file.translationFile);
        }
    }

    public TextAsset GetCurrentLanguageFile(LanguagesEnums.Language language)
    {
        return _translationsFileDictionary[language];
    }
}

[System.Serializable]
public struct TranslationsFiles
{
    public LanguagesEnums.Language language;
    public TextAsset translationFile;
}