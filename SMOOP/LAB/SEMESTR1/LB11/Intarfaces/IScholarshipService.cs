// ========================================================================
// [ISP] Interface Segregation Principle (Принцип розділення інтерфейсів)
// Замість одного великого інтерфейсу (наприклад, IUniversity), створено
// кілька дрібних. Клієнти, яким потрібна лише статистика, не будуть 
// залежати від методів стипендії або аналізу групи.
// ========================================================================

// [OCP] Open/Closed Principle (Принцип відкритості/закритості)
// Цей інтерфейс було додано для розширення функціоналу без зміни існуючого коду.
using System;
using System.Collections.Generic;
using System.Text;

namespace LB11
{
    public interface IScholarshipService
    {
        bool IsEligibleForScholarship(Student student);
    }
}
