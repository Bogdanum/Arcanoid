using System.Collections.Generic;
using Newtonsoft.Json;

public class JsonTranslationParser : ILanguageParser
{
     private JsonParserConfig _parserConfig;
     private LanguagesEnums.Language _currentLanguage;
     
     public JsonTranslationParser(JsonParserConfig parserConfig, LanguagesEnums.Language currentLanguage)
     {
          _parserConfig = parserConfig;
          _currentLanguage = currentLanguage;
     }

     public Dictionary<string, string> GetTranslationDictionary()
     {
          var translationFile = _parserConfig.GetCurrentLanguageFile(_currentLanguage);
          return JsonConvert.DeserializeObject<Dictionary<string, string>>(translationFile.text);
     }
}
