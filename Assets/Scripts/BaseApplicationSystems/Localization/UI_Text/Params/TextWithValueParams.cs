using UnityEngine;

[System.Serializable]
public class TextWithValueParams
{
    private const string defaultFormat = "{0}: {1}";
    
    [SerializeField] private bool isTextWithValue;
    [SerializeField] private string format = defaultFormat;

    public bool IsTextWithValue => isTextWithValue;
    public string Format => format;
}
