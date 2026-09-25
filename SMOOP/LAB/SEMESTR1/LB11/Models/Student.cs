// ========================================================================
// МОДЕЛІ ДАНИХ
// [SRP] Single Responsibility Principle (Принцип єдиної відповідальності)
// Класи Subject, Student та Group відповідають ТІЛЬКИ за збереження даних
// та базове управління власним станом. Вони не знають, як виводити себе
// в консоль і не знають, як рахувати середній бал.
// ========================================================================
using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace LB11
{
    public class Student
    {
        public int NumberInGroup { get; set; }
        public string FullName { get; set; }
        public string RecordBookNumber { get; set; }
        public string PhoneNumber { get; set; }

        public Subject[] Subjects { get; set; }
        public int SubjectCount { get; set; }

        public Student(int numberInGroup, string fullName, string recordBookNumber, string phoneNumber)
        {
            NumberInGroup = numberInGroup;
            FullName = fullName;
            RecordBookNumber = recordBookNumber;
            PhoneNumber = phoneNumber;

            Subjects = new Subject[7];
            SubjectCount = 0;
        }

        public void AddSubject(Subject subject)
        {
            if (SubjectCount < 7)
            {
                Subjects[SubjectCount] = subject;
                SubjectCount++;
            }
            else
            {
                Console.WriteLine($"Студент {FullName} вже має максимальну кількість оцінок (7).");
            }
        }
    }
}
