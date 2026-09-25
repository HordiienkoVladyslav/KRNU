using System;

namespace Module2
{
    // --- Завдання 2 ---
    public class StudentGroup
    {
        private Student[] students;

        public StudentGroup(int k)
        {
            students = new Student[k];
        }

        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException("Індекс поза межами групи.");
                return students[index];
            }
            set
            {
                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException("Індекс поза межами групи.");
                students[index] = value;
            }
        }

        public int Length => students.Length;

        public Student BestStudent
        {
            get
            {
                if (students == null || students.Length == 0) return null;

                Student best = students[0];
                for (int i = 1; i < students.Length; i++)
                {
                    if (students[i] != null && students[i].AverageGrade > best.AverageGrade)
                    {
                        best = students[i];
                    }
                }
                return best;
            }
        }
    }
}