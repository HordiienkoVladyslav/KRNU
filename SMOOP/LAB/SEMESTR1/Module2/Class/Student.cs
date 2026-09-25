using System;

namespace Module2
{
    // --- Завдання 1 ---
    public class Student
    {
        public int GroupNumber { get; set; }
        public string FullName { get; set; }
        public string RecordBookNumber { get; set; }
        public string Telephone { get; set; }

        private string[] subjects = new string[4];
        private int subjectsCount = 0;

        private int[] sessionGrades = new int[7];
        private int gradesCount = 0;

        public string[] Subjects => subjects;
        public int SubjectsCount => subjectsCount;
        public int[] SessionGrades => sessionGrades;
        public int GradesCount => gradesCount;

        public Student(int groupNumber, string fullName, string recordBookNumber, string telephone)
        {
            GroupNumber = groupNumber;
            FullName = fullName;
            RecordBookNumber = recordBookNumber;
            Telephone = telephone;
        }

        public void AddSubjectAndGrade(string subjectName, int grade)
        {
            if (gradesCount >= 7)
            {
                Console.WriteLine($"Не вдалося додати оцінку для {subjectName}: ліміт 7 оцінок вичерпано.");
                return;
            }

            if (subjectsCount == subjects.Length)
            {
                string[] temp = new string[subjects.Length * 2];
                Array.Copy(subjects, temp, subjects.Length);
                subjects = temp;
            }

            subjects[subjectsCount] = subjectName;
            subjectsCount++;

            sessionGrades[gradesCount] = grade;
            gradesCount++;
        }


        public double AverageGrade
        {
            get
            {
                if (gradesCount == 0) return 0.0;
                double sum = 0;
                for (int i = 0; i < gradesCount; i++)
                {
                    sum += sessionGrades[i];
                }
                return sum / gradesCount;
            }
        }

        public string[] HighestGradeSubjects
        {
            get
            {
                if (gradesCount == 0) return new string[0];

                int max = sessionGrades[0];
                for (int i = 1; i < gradesCount; i++)
                {
                    if (sessionGrades[i] > max) max = sessionGrades[i];
                }

                int count = 0;
                for (int i = 0; i < gradesCount; i++)
                {
                    if (sessionGrades[i] == max) count++;
                }

                string[] result = new string[count];
                int idx = 0;
                for (int i = 0; i < gradesCount; i++)
                {
                    if (sessionGrades[i] == max)
                    {
                        result[idx++] = subjects[i];
                    }
                }
                return result;
            }
        }

        public string[] LowestGradeSubjects
        {
            get
            {
                if (gradesCount == 0) return new string[0];

                int min = sessionGrades[0];
                for (int i = 1; i < gradesCount; i++)
                {
                    if (sessionGrades[i] < min) min = sessionGrades[i];
                }

                int count = 0;
                for (int i = 0; i < gradesCount; i++)
                {
                    if (sessionGrades[i] == min) count++;
                }

                string[] result = new string[count];
                int idx = 0;
                for (int i = 0; i < gradesCount; i++)
                {
                    if (sessionGrades[i] == min)
                    {
                        result[idx++] = subjects[i];
                    }
                }
                return result;
            }
        }
    }
}