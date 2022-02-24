using System.Collections.Generic;

public interface ILanguageParser
{
    Dictionary<string, List<string>> GetTranslationDictionary();
}
