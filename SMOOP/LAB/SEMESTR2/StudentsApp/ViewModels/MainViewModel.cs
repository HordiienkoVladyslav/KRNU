using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using StudentsApp.Infrastructure;
using StudentsApp.Models;
using StudentsApp.Services;

namespace StudentsApp.ViewModels;

public class MainViewModel : ViewModelBase, IDataErrorInfo
{
    public const int MinAge = 16;
    public const int MaxAge = 100;

    private readonly IDialogService _dialogService;
    private readonly StudentRepository _repository;

    // Поля, які користувач уже змінював: помилки показуємо лише для них,
    // щоб порожня форма не світилася червоним одразу після запуску.
    private readonly HashSet<string> _touchedFields = new();

    // Під час масових змін IsSelected не реагуємо на кожну окрему подію.
    private bool _suppressSelection;

    private Student? _selectedStudent;
    private int _selectedCount;
    private string _editLastName = string.Empty;
    private string _editFirstName = string.Empty;
    private string _editAge = string.Empty;
    private string? _editGender;
    private string _statusMessage = string.Empty;

    public MainViewModel() : this(new MessageBoxDialogService(), new StudentRepository())
    {
    }

    public MainViewModel(IDialogService dialogService, StudentRepository repository)
    {
        _dialogService = dialogService;
        _repository = repository;

        Students = new ObservableCollection<Student>(RemoveDuplicates(_repository.Load()));
        foreach (var student in Students)
            student.PropertyChanged += OnStudentPropertyChanged;
        Students.CollectionChanged += OnStudentsCollectionChanged;

        AddCommand = new RelayCommand(Add, () => !HasFieldErrors && !IsDuplicate(excludeSelected: false));
        SaveCommand = new RelayCommand(Save, () => SelectedStudent is not null && !HasFieldErrors && !IsDuplicate(excludeSelected: true));
        DeleteCommand = new RelayCommand(Delete, () => Students.Count > 0 && SelectedCount > 0);
        NewCommand = new RelayCommand(NewEntry);
        SelectAllCommand = new RelayCommand(SelectAll, () => Students.Count > 0);
    }

    // ---------- Дані ----------

    public ObservableCollection<Student> Students { get; }

    public IReadOnlyList<string> Genders { get; } = new[] { "Чоловіча", "Жіноча" };

    /// <summary>Єдиний вибраний студент (для редагування). Якщо вибрано 0 або кілька — null.</summary>
    public Student? SelectedStudent => _selectedStudent;

    /// <summary>Кількість вибраних у списку студентів.</summary>
    public int SelectedCount
    {
        get => _selectedCount;
        private set => SetProperty(ref _selectedCount, value);
    }

    public string EditLastName
    {
        get => _editLastName;
        set
        {
            if (SetProperty(ref _editLastName, value))
                OnEditFieldChanged(nameof(EditLastName));
        }
    }

    public string EditFirstName
    {
        get => _editFirstName;
        set
        {
            if (SetProperty(ref _editFirstName, value))
                OnEditFieldChanged(nameof(EditFirstName));
        }
    }

    public string EditAge
    {
        get => _editAge;
        set
        {
            if (SetProperty(ref _editAge, value))
                OnEditFieldChanged(nameof(EditAge));
        }
    }

    public string? EditGender
    {
        get => _editGender;
        set
        {
            if (SetProperty(ref _editGender, value))
                OnEditFieldChanged(nameof(EditGender));
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        private set => SetProperty(ref _statusMessage, value);
    }

    /// <summary>Повідомлення про дублікат (порожнє, якщо дубліката немає).</summary>
    public string DuplicateError =>
        _touchedFields.Count > 0 && !HasFieldErrors && IsDuplicate(excludeSelected: true)
            ? "Такий запис уже існує в списку."
            : string.Empty;

    // ---------- Команди ----------

    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectAllCommand { get; }

    private void Add()
    {
        if (IsDuplicate(excludeSelected: false))
        {
            _dialogService.ShowError("Такий запис уже існує в списку.", "Дублікат");
            return;
        }

        var student = new Student
        {
            LastName = EditLastName.Trim(),
            FirstName = EditFirstName.Trim(),
            Age = int.Parse(EditAge),
            Gender = EditGender!,
        };

        Students.Add(student);
        PersistAndReport("Студента додано.");
        NewEntry();
    }

    private void Save()
    {
        if (SelectedStudent is null)
            return;

        if (IsDuplicate(excludeSelected: true))
        {
            _dialogService.ShowError("Такий запис уже існує в списку.", "Дублікат");
            return;
        }

        SelectedStudent.LastName = EditLastName.Trim();
        SelectedStudent.FirstName = EditFirstName.Trim();
        SelectedStudent.Age = int.Parse(EditAge);
        SelectedStudent.Gender = EditGender!;

        PersistAndReport("Зміни збережено.");
        OnPropertyChanged(nameof(DuplicateError));
    }

    private void Delete()
    {
        var toDelete = Students.Where(s => s.IsSelected).ToList();
        if (toDelete.Count == 0)
            return;

        var message = toDelete.Count == 1
            ? $"Видалити студента «{toDelete[0].LastName} {toDelete[0].FirstName}»?"
            : $"Видалити вибраних студентів ({toDelete.Count})?";

        if (!_dialogService.Confirm(message, "Підтвердження видалення"))
            return;

        _suppressSelection = true;
        try
        {
            foreach (var student in toDelete)
                Students.Remove(student);
        }
        finally
        {
            _suppressSelection = false;
        }

        UpdateSelection();
        PersistAndReport(toDelete.Count == 1 ? "Студента видалено." : $"Видалено студентів: {toDelete.Count}.");
    }

    private void NewEntry()
    {
        _suppressSelection = true;
        try
        {
            foreach (var student in Students.Where(s => s.IsSelected))
                student.IsSelected = false;
        }
        finally
        {
            _suppressSelection = false;
        }

        UpdateSelection();
        ClearEditFields();
    }

    private void SelectAll()
    {
        _suppressSelection = true;
        try
        {
            foreach (var student in Students)
                student.IsSelected = true;
        }
        finally
        {
            _suppressSelection = false;
        }

        UpdateSelection();
    }

    private void PersistAndReport(string successMessage)
    {
        try
        {
            _repository.Save(Students);
            StatusMessage = successMessage;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or InvalidOperationException)
        {
            StatusMessage = "Не вдалося зберегти файл.";
            _dialogService.ShowError($"Не вдалося зберегти Students.xml:\n{ex.Message}", "Помилка збереження");
        }
    }

    // ---------- Вибір елементів у списку ----------

    private void OnStudentPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_suppressSelection || e.PropertyName != nameof(Student.IsSelected))
            return;

        UpdateSelection();
    }

    private void OnStudentsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
            foreach (Student s in e.OldItems)
                s.PropertyChanged -= OnStudentPropertyChanged;

        if (e.NewItems is not null)
            foreach (Student s in e.NewItems)
                s.PropertyChanged += OnStudentPropertyChanged;

        OnPropertyChanged(nameof(DuplicateError));
        RelayCommand.RaiseCanExecuteChanged();
    }

    /// <summary>
    /// Перераховує вибір: рівно один вибраний — завантажуємо його у форму редагування,
    /// інакше форму очищаємо (редагувати можна лише один запис, видаляти — кілька).
    /// </summary>
    private void UpdateSelection()
    {
        var selected = Students.Where(s => s.IsSelected).ToList();
        SelectedCount = selected.Count;

        var single = selected.Count == 1 ? selected[0] : null;
        if (!ReferenceEquals(_selectedStudent, single))
        {
            _selectedStudent = single;
            OnPropertyChanged(nameof(SelectedStudent));
        }

        if (single is not null)
            LoadEditFields(single);
        else
            ClearEditFields();
    }

    // ---------- Форма редагування ----------

    private void LoadEditFields(Student student)
    {
        _editLastName = student.LastName;
        _editFirstName = student.FirstName;
        _editAge = student.Age.ToString();
        _editGender = student.Gender;
        _touchedFields.Clear();
        RaiseEditFieldsChanged();
    }

    private void ClearEditFields()
    {
        _editLastName = string.Empty;
        _editFirstName = string.Empty;
        _editAge = string.Empty;
        _editGender = null;
        _touchedFields.Clear();
        RaiseEditFieldsChanged();
    }

    private void RaiseEditFieldsChanged()
    {
        OnPropertyChanged(nameof(EditLastName));
        OnPropertyChanged(nameof(EditFirstName));
        OnPropertyChanged(nameof(EditAge));
        OnPropertyChanged(nameof(EditGender));
        OnPropertyChanged(nameof(DuplicateError));
        RelayCommand.RaiseCanExecuteChanged();
    }

    private void OnEditFieldChanged(string fieldName)
    {
        _touchedFields.Add(fieldName);
        StatusMessage = string.Empty;
        OnPropertyChanged(nameof(DuplicateError));
        RelayCommand.RaiseCanExecuteChanged();
    }

    // ---------- Дублікати ----------

    /// <summary>
    /// Чи є у списку запис, що збігається з даними форми.
    /// excludeSelected = true — не рахувати редагований запис (для «Зберегти зміни»).
    /// </summary>
    private bool IsDuplicate(bool excludeSelected)
    {
        if (!int.TryParse(EditAge, out var age))
            return false;

        return Students.Any(s =>
            (!excludeSelected || !ReferenceEquals(s, SelectedStudent))
            && s.IsSameAs(EditLastName, EditFirstName, age, EditGender));
    }

    private static List<Student> RemoveDuplicates(IEnumerable<Student> source)
    {
        var unique = new List<Student>();
        foreach (var s in source)
        {
            if (!unique.Any(u => u.IsSameAs(s.LastName, s.FirstName, s.Age, s.Gender)))
                unique.Add(s);
        }

        return unique;
    }

    // ---------- Валідація полів (IDataErrorInfo) ----------

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            // Помилку показуємо лише для полів, які користувач уже змінював.
            return _touchedFields.Contains(columnName)
                ? Validate(columnName)
                : string.Empty;
        }
    }

    /// <summary>Чи є помилки в будь-якому полі форми (незалежно від того, чи редагувалося поле).</summary>
    private bool HasFieldErrors =>
        !string.IsNullOrEmpty(Validate(nameof(EditLastName))) ||
        !string.IsNullOrEmpty(Validate(nameof(EditFirstName))) ||
        !string.IsNullOrEmpty(Validate(nameof(EditAge))) ||
        !string.IsNullOrEmpty(Validate(nameof(EditGender)));

    private string Validate(string columnName) => columnName switch
    {
        nameof(EditLastName) when string.IsNullOrWhiteSpace(EditLastName)
            => "Прізвище є обов'язковим.",

        nameof(EditFirstName) when string.IsNullOrWhiteSpace(EditFirstName)
            => "Ім'я є обов'язковим.",

        nameof(EditGender) when string.IsNullOrWhiteSpace(EditGender)
            => "Оберіть стать.",

        nameof(EditAge) => ValidateAge(),

        _ => string.Empty,
    };

    private string ValidateAge()
    {
        if (string.IsNullOrWhiteSpace(EditAge))
            return "Вік є обов'язковим.";

        if (!int.TryParse(EditAge, out var age))
            return "Вік має бути цілим числом.";

        if (age < 0)
            return "Вік не може бути від'ємним.";

        if (age < MinAge || age > MaxAge)
            return $"Вік має бути від {MinAge} до {MaxAge} років.";

        return string.Empty;
    }
}
