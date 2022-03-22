using UnityEngine;

[System.Serializable]
public class TextWithValueParams
{
    private const string defaultFormat = "{0}: {1}";
    
    [SerializeField] private bool isTextWithValue;
    [SerializeField] private string format = defaultFormat;

    public bool IsTextWithValue { get => isTextWithValue; set => isTextWithValue = value; }
    public string Format => format;
}
