using Module2;
using System;
using System.Text;

namespace StudentSessionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StudentGroup group = new StudentGroup(3);

            group[0] = new Student(101, "Іваненко І.І.", "ЗБ-01", "050");
            group[0].AddSubjectAndGrade("Математика", 95);
            group[0].AddSubjectAndGrade("Фізика", 60);      
            group[0].AddSubjectAndGrade("Програмування", 95); 

            group[1] = new Student(101, "Петренко П.П.", "ЗБ-02", "067");
            group[1].AddSubjectAndGrade("Математика", 45); 
            group[1].AddSubjectAndGrade("Фізика", 70);
            group[1].AddSubjectAndGrade("Програмування", 70);

            group[2] = new Student(101, "Сидоренко С.С.", "ЗБ-03", "093");
            group[2].AddSubjectAndGrade("Математика", 98);
            group[2].AddSubjectAndGrade("Фізика", 95);
            group[2].AddSubjectAndGrade("Програмування", 99); 

            Console.WriteLine("=== Перевірка властивостей студентів (Завдання 1) ===");
            for (int i = 0; i < group.Length; i++)
            {
                Student s = group[i];
                Console.WriteLine($"\nСтудент: {s.FullName}");
                Console.WriteLine($" Середній бал: {s.AverageGrade:F2}");

                Console.Write(" Предмети з найвищим балом: ");
                Console.WriteLine(string.Join(", ", s.HighestGradeSubjects));

                Console.Write(" Предмети з найнижчим балом: ");
                Console.WriteLine(string.Join(", ", s.LowestGradeSubjects));
            }

            Console.WriteLine("\n--------------------------------------------------");

            Console.WriteLine("=== Перевірка властивостей групи (Завдання 2) ===");

            Student leader = group.BestStudent; 
            if (leader != null)
            {
                Console.WriteLine($"\nСамий успішний студент групи: {leader.FullName}");
                Console.WriteLine($"Його загальна успішність: {leader.AverageGrade:F2}");
            }
        }
    }
}