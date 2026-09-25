// ========================================================================
// КЕРУВАННЯ ІНТЕРФЕЙСОМ КОРИСТУВАЧА
// [SRP] Single Responsibility Principle
// Цей клас відповідає ТІЛЬКИ за відображення меню та зчитування даних 
// з консолі. Бізнес-логіка сюди передається через інтерфейси.
// ========================================================================
using LB11;

public class MenuHandler
{
    private readonly Group _group;
    private readonly IStudentStatistics _statsService;
    private readonly IGroupAnalyzer _analyzerService;
    private readonly IScholarshipService _scholarshipService;

    // [DIP] Інверсія залежностей: меню працює з абстракціями сервісів
    public MenuHandler(
        Group group,
        IStudentStatistics statsService,
        IGroupAnalyzer analyzerService,
        IScholarshipService scholarshipService)
    {
        _group = group;
        _statsService = statsService;
        _analyzerService = analyzerService;
        _scholarshipService = scholarshipService;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n================ МЕНЮ КЕРУВАННЯ ================");
            Console.WriteLine("1. Вивести список усієї групи та її студентів");
            Console.WriteLine("2. Вивести Топ-3 найуспішніших студентів групи");
            Console.WriteLine("3. Переглянути детальну успішність по конкретному студенту");
            Console.WriteLine("4. Вивести студентів, які отримують стипендію");
            Console.WriteLine("0. Вийти з програми");
            Console.WriteLine("==================================================");
            Console.Write("Оберіть дію: ");

            int choice = 0;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Некоректне введення. Оберіть пункт меню (0-4): ");
            }

            switch (choice)
            {
                case 1:
                    ShowAllStudents();
                    break;
                case 2:
                    ShowTopStudents();
                    break;
                case 3:
                    ShowDetailedStudentInfo();
                    break;
                case 4:
                    ShowScholarshipStudents();
                    break;
                case 0:
                    running = false;
                    Console.WriteLine("Програма завершує роботу. До побачення!");
                    break;
                default:
                    Console.WriteLine("Такого пункту меню не існує. Спробуйте ще раз.");
                    break;
            }
        }
    }

    private void ShowAllStudents()
    {
        Console.WriteLine("\n--- Список усіх студентів групи ---");
        if (_group.StudentCount == 0)
        {
            Console.WriteLine("Група порожня.");
            return;
        }

        int i = 0;
        while (i < _group.StudentCount)
        {
            Student currentStudent = _group.Students[i];
            double average = Math.Round(_statsService.CalculateAverage(currentStudent), 2);
            Console.WriteLine($"[{i + 1}] {currentStudent.FullName} (Залікова: {currentStudent.RecordBookNumber}) — Середній бал: {average}");
            i++;
        }
    }

    private void ShowTopStudents()
    {
        Console.WriteLine("\n--- Топ-3 найуспішніших студентів ---");
        Student[] topStudentsArr = _analyzerService.GetTopStudents(_group, 3);

        if (topStudentsArr.Length == 0)
        {
            Console.WriteLine("Немає даних для виведення.");
            return;
        }

        int i = 0;
        while (i < topStudentsArr.Length)
        {
            Student currentStudent = topStudentsArr[i];
            double average = Math.Round(_statsService.CalculateAverage(currentStudent), 2);
            Console.WriteLine($"{i + 1} місце: {currentStudent.FullName} — Середній бал: {average}");
            i++;
        }
    }

    private void ShowDetailedStudentInfo()
    {
        ShowAllStudents();
        if (_group.StudentCount == 0) return;

        Console.Write($"\nВведіть порядковий номер студента (1-{_group.StudentCount}): ");
        int index = 0;

        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > _group.StudentCount)
        {
            Console.Write($"Некоректний номер. Введіть число від 1 до {_group.StudentCount}: ");
        }

        Student selectedStudent = _group.Students[index - 1];

        Console.WriteLine($"\n=== Детальна інформація про студента ===");
        Console.WriteLine($"ПІП: {selectedStudent.FullName}");
        Console.WriteLine($"Номер у групі: {selectedStudent.NumberInGroup}");
        Console.WriteLine($"Номер залікової книжки: {selectedStudent.RecordBookNumber}");
        Console.WriteLine($"Телефон: {selectedStudent.PhoneNumber}");

        double average = Math.Round(_statsService.CalculateAverage(selectedStudent), 2);
        Console.WriteLine($"Загальна успішність (Середній бал): {average}");

        Subject[] bestArr = _statsService.GetBestSubjects(selectedStudent);
        Console.Write("Найкращі дисципліни: ");
        int j = 0;
        while (j < bestArr.Length)
        {
            Console.Write($"'{bestArr[j].Name}' ({bestArr[j].Score}) ");
            j++;
        }
        Console.WriteLine();

        Subject[] worstArr = _statsService.GetWorstSubjects(selectedStudent);
        Console.Write("Найслабші дисципліни: ");
        j = 0;
        while (j < worstArr.Length)
        {
            Console.Write($"'{worstArr[j].Name}' ({worstArr[j].Score}) ");
            j++;
        }
        Console.WriteLine();

        bool eligibility = _scholarshipService.IsEligibleForScholarship(selectedStudent);
        Console.WriteLine($"Чи виходить на стипендію: {(eligibility ? "Так, успішно претендує" : "Ні, є оцінки нижче 75 балів")}");
        Console.WriteLine("========================================");
    }

    private void ShowScholarshipStudents()
    {
        Console.WriteLine("\n--- Студенти, які отримують стипендію ---");
        if (_group.StudentCount == 0)
        {
            Console.WriteLine("Група порожня.");
            return;
        }

        int i = 0;
        int eligibleCount = 0;

        while (i < _group.StudentCount)
        {
            Student currentStudent = _group.Students[i];

            if (_scholarshipService.IsEligibleForScholarship(currentStudent))
            {
                double average = Math.Round(_statsService.CalculateAverage(currentStudent), 2);
                Console.WriteLine($"- {currentStudent.FullName} (Середній бал: {average})");
                eligibleCount++;
            }
            i++;
        }

        if (eligibleCount == 0)
        {
            Console.WriteLine("У цій групі жоден студент не набрав достатньо балів для стипендії.");
        }
        else
        {
            Console.WriteLine($"\nЗагалом претендентів на стипендію: {eligibleCount}");
        }
    }
}