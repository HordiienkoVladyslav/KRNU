using System.Collections.Generic;

public class DictionaryItem
{
    public string Word { get; set; } = string.Empty;
    public List<string> Translations { get; set; } = new();
}

public class TranslationDictionary
{
    public string DictionaryName { get; set; } = string.Empty;
    public List<DictionaryItem> Items { get; set; } = new();
}