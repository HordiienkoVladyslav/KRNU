# Лабораторна робота №3 — Взаємодія з файловою системою (System.IO)

Консольний застосунок на **C# 14 / .NET 10**.

## Запуск

```bash
cd Lab3FileSystem
dotnet run
```

Тестові файли лежать у `SampleData/` (`SampleData/sample_text.txt`, `SampleData/bad_words.txt`).

## Структура проєкту

```
Lab3FileSystem/                    ← корінь рішення
├── Lab3FileSystem.slnx
└── Lab3FileSystem/                ← проєкт
    ├── Lab3FileSystem.csproj
    ├── Program.cs                 ← точка входу
    ├── Common/
    │   ├── ConsoleHelper.cs       ← введення з консолі
    │   └── FileHelper.cs          ← читання/запис UTF-8
    ├── UI/
    │   └── MainMenu.cs            ← головне меню
    ├── Task1_TextStatistics/
    │   ├── TextStatistics.cs      ← record з результатами
    │   ├── TextAnalyzer.cs        ← логіка підрахунку
    │   └── TextStatisticsMenu.cs  ← консольний інтерфейс
    ├── Task2_Censor/
    │   ├── TextCensor.cs          ← логіка цензури
    │   └── CensorMenu.cs          ← консольний інтерфейс
    ├── Task3_Songs/
    │   ├── Models/                ← Song, SongUpdate, SearchField, EditResult
    │   ├── Services/              ← SongCollectionManager (CRUD, пошук, JSON)
    │   └── UI/                    ← SongMenu
    └── SampleData/                ← тестові файли
```
