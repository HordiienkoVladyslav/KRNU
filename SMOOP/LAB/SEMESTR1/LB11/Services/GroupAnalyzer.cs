// ========================================================================
// СЕРВІСИ (БІЗНЕС-ЛОГІКА)
// [SRP] Single Responsibility Principle
// Кожен сервіс виконує лише одну конкретну задачу.
// ========================================================================
// [DIP] Dependency Inversion Principle (Принцип інверсії залежностей)
// GroupAnalyzer залежить від абстракції (IStudentStatistics), а не від 
// конкретного класу StudentStatistics. Це дозволяє легко підмінити логіку.
//
// [LSP] Liskov Substitution Principle (Принцип підстановки Лісков)
// Сюди можна передати БУДЬ-ЯКИЙ об'єкт, що реалізує IStudentStatistics, 
// і цей клас продовжить працювати коректно, не знаючи деталей реалізації.
using System;
using System.Collections.Generic;
using System.Text;

namespace LB11
{
    public class GroupAnalyzer : IGroupAnalyzer
    {
        private readonly IStudentStatistics _statistics;

        public GroupAnalyzer(IStudentStatistics statistics)
        {
            _statistics = statistics;
        }

        public Student[] GetTopStudents(Group group, int count)
        {
            int n = group.StudentCount;
            Student[] arr = new Student[n];

            for (int i = 0; i < n; i++)
            {
                arr[i] = group.Students[i];
            }

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (_statistics.CalculateAverage(arr[j]) < _statistics.CalculateAverage(arr[j + 1]))
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }
                }
            }

            int resultCount = count < n ? count : n;
            Student[] topStudentsArr = new Student[resultCount];
            for (int i = 0; i < resultCount; i++)
            {
                topStudentsArr[i] = arr[i];
            }

            return topStudentsArr;
        }
    }
}
