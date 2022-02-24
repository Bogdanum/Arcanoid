using UnityEngine;

public class LanguageParserConfig : ScriptableObject
{
    [SerializeField] private string translateFilePath;
    public string TranslateFilePath => translateFilePath;
}
