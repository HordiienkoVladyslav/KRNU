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
    public class Group
    {
        public Student[] Students { get; set; }
        public int StudentCount { get; set; }

        public Group(int capacity)
        {
            Students = new Student[capacity];
            StudentCount = 0;
        }

        public void AddStudent(Student student)
        {
            if (StudentCount < Students.Length)
            {
                Students[StudentCount] = student;
                StudentCount++;
            }
            else
            {
                Console.WriteLine("Група повністю заповнена.");
            }
        }
    }
}
