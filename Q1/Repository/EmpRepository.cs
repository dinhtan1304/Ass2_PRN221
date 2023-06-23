using Q1.Models;
using System.Collections.Generic;

namespace Q1.Repository
{
    public class EmpRepository : IEmpRepository
    {
        public void DeleteEmp(Employee employee) => EmployeeManager.Instance.Remove(employee);

        public Employee GetEmpById(int empId) => EmployeeManager.Instance.GetEmpByID(empId);

        public IEnumerable<Employee> GetEmps() => EmployeeManager.Instance.GetEmpList();

        public void InsertEmp(Employee employee) => EmployeeManager.Instance.AddNew(employee);

        public void UpdateEmp(Employee employee) => EmployeeManager.Instance.Update(employee);
    }
}
