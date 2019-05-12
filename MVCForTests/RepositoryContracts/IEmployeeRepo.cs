using MVCForTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCForTests.RepositoryContracts
{
    public interface IEmployeeRepo
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int? id);
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
