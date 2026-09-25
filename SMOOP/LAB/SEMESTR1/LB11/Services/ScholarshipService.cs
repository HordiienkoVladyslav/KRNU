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
    public class ScholarshipService : IScholarshipService
    {
        public bool IsEligibleForScholarship(Student student)
        {
            if (student.SubjectCount == 0) return false;

            for (int i = 0; i < student.SubjectCount; i++)
            {
                if (student.Subjects[i].Score < 75)
                {
                    return false;
                }
            }
            return true;
        }
    }
}