using MVCForTests.Models;
using MVCForTests.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForTests.EmployeeRepository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        public Employee GetEmployee(int id)
        {
            return db.Employees.FirstOrDefault(e => e.Id.Equals(id));
        }

        public void InsertEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            Employee empFromDb = db.Employees.FirstOrDefault(e => e.Id.Equals(employee.Id));
            empFromDb.FirstName = employee.FirstName;
            empFromDb.LastName = employee.LastName;
            empFromDb.Email = employee.Email;
            empFromDb.Gender = employee.Gender;
            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            Employee emp = db.Employees.FirstOrDefault(e => e.Id.Equals(id));
            db.Employees.Remove(emp);
            db.SaveChanges();
        }

    }
}