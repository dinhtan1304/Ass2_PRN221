using Q1.Models;
using System.Collections.Generic;

namespace Q1.Repository
{
    public interface IEmpRepository
    {
        IEnumerable<Employee> GetEmps();
        Employee GetEmpById(int empId);
        void InsertEmp(Employee employee);
        void UpdateEmp(Employee employee);
        void DeleteEmp(Employee employee);
    }
}
