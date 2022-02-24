using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CsvParser : ILanguageParser
{
    private CsvParserConfig _parserConfig;

    public CsvParser(CsvParserConfig parserConfig)
    {
        _parserConfig = parserConfig;
    }

    public Dictionary<string, List<string>> GetTranslationDictionary() => ParseCsvFile();

    private Dictionary<string, List<string>> ParseCsvFile()
    {
        var outputDictionary = new Dictionary<string, List<string>>();
        string[] lines = GetLinesFromCSV();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = SplitLine(lines[i]);
            if (row.Any())
            {
                List<string> words = new List<string>(row);
                words.RemoveAt(0);
                outputDictionary.Add(row[0], words);
            }
        }
        return outputDictionary;
    }

    private string[] GetLinesFromCSV()
    {
        var csvFile = Resources.Load<TextAsset>(_parserConfig.TranslateFilePath);
        string[] lines = csvFile.text.Split("\n"[0]);
        return lines;
    }
    
    private string[] SplitLine(string line)
    {
        var newLine = line.Split(_parserConfig.IgnoredCharacters, StringSplitOptions.RemoveEmptyEntries);
        return newLine;
    }
}
