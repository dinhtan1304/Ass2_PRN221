using System;
using System.Collections.Generic;
using System.Linq;

namespace Q1.Models
{
    public class EmployeeManager
    {
        private static EmployeeManager instance = null;
        private static readonly object instanceLock = new object();
        private EmployeeManager() { }
        public static EmployeeManager Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new EmployeeManager();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Employee> GetEmpList()
        {
            List<Employee> employees;
            try
            {
                var myStockDB = new Prn221TrialContext();
                employees = myStockDB.Employees.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return employees;
        }

        public Employee GetEmpByID(int empId)
        {
            Employee employee = null;
            try
            {
                var myStockDB = new Prn221TrialContext();
                employee = myStockDB.Employees.SingleOrDefault(emp => emp.Id == empId);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return employee;
        }

        public void AddNew(Employee employee)
        {
            try
            {
                Employee _employee = GetEmpByID(employee.Id);
                if (_employee == null)
                {
                    var myStockDB = new Prn221TrialContext();
                    myStockDB.Employees.Add(employee);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The employee is already exist.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }

        public void Update(Employee employee)
        {
            try
            {
                Employee _employee = GetEmpByID(employee.Id);
                if (_employee != null)
                {
                    var myStockDB = new Prn221TrialContext();
                    myStockDB.Entry<Employee>(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The employee does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Employee employee)
        {
            try
            {
                Employee _employee = GetEmpByID(employee.Id);

                if (_employee != null)
                {
                    var myStockDB = new Prn221TrialContext();
                    myStockDB.Employees.Remove(employee);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The employee does not exist.");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
    }
}
