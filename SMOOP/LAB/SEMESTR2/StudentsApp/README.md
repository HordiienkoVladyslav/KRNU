# StudentsApp — WPF, MVVM, Data Binding (.NET 10)

Простий застосунок для керування списком студентів: додавання, редагування, видалення, валідація.

## Вимоги

- Windows 10/11 (WPF працює лише на Windows)
- .NET 10 SDK: https://dotnet.microsoft.com/download/dotnet/10.0
- Visual Studio з підтримкою .NET 10 (робоче навантаження «.NET desktop development») — або лише .NET SDK і термінал

Перевірити SDK: `dotnet --version` (має починатися з `10.`).

## Запуск

З терміналу:

```
cd StudentsApp
dotnet run
```

У Visual Studio: відкрийте `StudentsApp.csproj` (File → Open → Project/Solution) і натисніть F5.

Збірка виконуваного файлу: `dotnet publish -c Release -r win-x64 --self-contained false`.

## Структура

```
StudentsApp/
├── StudentsApp.csproj
├── App.xaml / App.xaml.cs
├── MainWindow.xaml            View (розмітка, Binding, стилі)
├── MainWindow.xaml.cs         лише InitializeComponent()
├── Students.xml               початкові дані
├── Models/Student.cs          Model
├── ViewModels/
│   ├── ViewModelBase.cs
│   └── MainViewModel.cs       ViewModel: стан, команди, IDataErrorInfo
├── Infrastructure/
│   ├── ObservableObject.cs    INotifyPropertyChanged
│   └── RelayCommand.cs        ICommand
└── Services/
    ├── IDialogService.cs
    ├── MessageBoxDialogService.cs   діалог підтвердження
    └── StudentRepository.cs         XML-завантаження/збереження
```

## Де що реалізовано

| Вимога | Реалізація |
|---|---|
| `{Binding}`, `DataContext` | `MainWindow.xaml` (`Window.DataContext`) |
| `INotifyPropertyChanged` | `ObservableObject` → `ViewModelBase`, `Student` |
| `ObservableCollection<T>` | `MainViewModel.Students` |
| Команди | `RelayCommand`: Додати, Зберегти зміни, Видалити, Очистити |
| Валідація | `IDataErrorInfo` у `MainViewModel` (обов'язкові поля, вік 16–100) |
| «Прізвище Ім'я» в одному рядку | `MultiBinding` з `StringFormat` |
| Вік із «років» | `StringFormat={}{0} років` |
| Порожній список | кнопки «Зберегти»/«Видалити» блокуються через `CanExecute`; замість списку — текст |
| Чергування кольорів | `AlternationCount="2"` + `ItemsControl.AlternationIndex` |
| Підтвердження видалення | `IDialogService.Confirm` (MessageBox Так/Ні) |
| Видалення кількох записів | `ListBox SelectionMode="Extended"` (Ctrl/Shift + клік, кнопка «Вибрати всіх»); вибір передається у ViewModel через `Student.IsSelected`, прив'язану до `ListBoxItem.IsSelected` |
| Заборона дублікатів | `MainViewModel.IsDuplicate`: запис збігається за прізвищем, ім'ям, віком і статтю (без урахування регістру); перевіряється при додаванні, збереженні змін і завантаженні з XML |

## Примітки

- Дані читаються з `Students.xml` поруч із `.exe` (копіюється при збірці) і зберігаються туди ж після кожної зміни. Якщо файл відсутній, використовується початковий список.
- Редагувати можна один запис (коли вибрано рівно один рядок); видаляти можна один або кілька. Для кількох вибраних форма редагування очищається.
- Помилки валідації з'являються під полем після першої зміни цього поля; кнопки «Додати»/«Зберегти зміни» неактивні, поки є помилки.
