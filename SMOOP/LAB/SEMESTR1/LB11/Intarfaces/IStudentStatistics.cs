// ========================================================================
// [ISP] Interface Segregation Principle (Принцип розділення інтерфейсів)
// Замість одного великого інтерфейсу (наприклад, IUniversity), створено
// кілька дрібних. Клієнти, яким потрібна лише статистика, не будуть 
// залежати від методів стипендії або аналізу групи.
// ========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LB11
{
    public interface IStudentStatistics
    {
        double CalculateAverage(Student student);
        Subject[] GetBestSubjects(Student student);
        Subject[] GetWorstSubjects(Student student);
    }
}
