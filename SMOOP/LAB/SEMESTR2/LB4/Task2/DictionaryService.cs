using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class DictionaryService
{
    private Dictionary<string, TranslationDictionary> _dictionaries = new();
    private const string StorageFile = "dictionaries_data.json";

    public DictionaryService()
    {
        LoadData();
    }

    public Dictionary<string, TranslationDictionary> Dictionaries => _dictionaries;

    public void CreateDictionary(string name)
    {
        if (!string.IsNullOrEmpty(name) && !_dictionaries.ContainsKey(name))
        {
            _dictionaries[name] = new TranslationDictionary { DictionaryName = name, Items = new() };
            SaveData();
            Console.WriteLine($"Словник '{name}' успішно створено!");
        }
        else
        {
            Console.WriteLine("Словник з такою назвою вже існує або назва порожня.");
        }
    }

    public void AddWord(TranslationDictionary dict, string word, string translation)
    {
        var item = dict.Items.FirstOrDefault(i => i.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
        if (item == null)
        {
            item = new DictionaryItem { Word = word, Translations = new() };
            dict.Items.Add(item);
        }

        if (!string.IsNullOrEmpty(translation) && !item.Translations.Contains(translation, StringComparer.OrdinalIgnoreCase))
        {
            item.Translations.Add(translation);
            SaveData();
            Console.WriteLine("Переклад успішно додано!");
        }
        else
        {
            Console.WriteLine("Такий переклад вже існує або він порожній.");
        }
    }

    public void SaveData()
    {
        string json = JsonSerializer.Serialize(_dictionaries, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(StorageFile, json);
    }

    public void LoadData()
    {
        if (File.Exists(StorageFile))
        {
            try
            {
                string json = File.ReadAllText(StorageFile);
                _dictionaries = JsonSerializer.Deserialize<Dictionary<string, TranslationDictionary>>(json) ?? new();
            }
            catch
            {
                _dictionaries = new();
            }
        }
    }
}