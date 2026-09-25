// ========================================================================
// СЕРВІСИ (БІЗНЕС-ЛОГІКА)
// [SRP] Single Responsibility Principle
// Кожен сервіс виконує лише одну конкретну задачу.
// ========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LB11
{
    public class StudentStatistics : IStudentStatistics
    {
        public double CalculateAverage(Student student)
        {
            if (student.SubjectCount == 0) return 0;

            double sum = 0;
            for (int i = 0; i < student.SubjectCount; i++)
            {
                sum += student.Subjects[i].Score;
            }
            return sum / student.SubjectCount;
        }

        public Subject[] GetBestSubjects(Student student)
        {
            if (student.SubjectCount == 0) return new Subject[0];

            int maxScore = student.Subjects[0].Score;
            for (int i = 1; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score > maxScore)
                {
                    maxScore = student.Subjects[i].Score;
                }
            }

            int count = 0;
            for (int i = 0; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score == maxScore) count++;
            }

            Subject[] arr = new Subject[count];
            int index = 0;
            for (int i = 0; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score == maxScore)
                {
                    arr[index] = student.Subjects[i];
                    index++;
                }
            }
            return arr;
        }

        public Subject[] GetWorstSubjects(Student student)
        {
            if (student.SubjectCount == 0) return new Subject[0];

            int minScore = student.Subjects[0].Score;
            for (int i = 1; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score < minScore)
                {
                    minScore = student.Subjects[i].Score;
                }
            }

            int count = 0;
            for (int i = 0; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score == minScore) count++;
            }

            Subject[] arr = new Subject[count];
            int index = 0;
            for (int i = 0; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score == minScore)
                {
                    arr[index] = student.Subjects[i];
                    index++;
                }
            }
            return arr;
        }
    }
}