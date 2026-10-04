using System.Text;

namespace Lab3FileSystem.Common;

/// <summary>Читання та запис текстових файлів у кодуванні UTF-8.</summary>
public static class FileHelper
{
    /// <summary>Зчитує весь текст файлу (UTF-8).</summary>
    public static string ReadAllText(string path)
    {
        // using declaration: потік закриється автоматично наприкінці методу
        using var reader = new StreamReader(path, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    /// <summary>Записує текст у файл (UTF-8 без BOM), перезаписуючи вміст.</summary>
    public static void WriteAllText(string path, string content)
    {
        using var writer = new StreamWriter(path, append: false, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
        writer.Write(content);
    }
}
