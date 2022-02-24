using UnityEngine;

[CreateAssetMenu(fileName = "CsvParserConfig", menuName = "Localization/ParserConfiguration")]
public class CsvParserConfig : LanguageParserConfig
{
     [SerializeField] private char[] ignoredCharacters;
     public char[] IgnoredCharacters => ignoredCharacters;
}
