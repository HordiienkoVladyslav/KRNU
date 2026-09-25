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

namespace LB11
{
    public class Subject
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Subject(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}
