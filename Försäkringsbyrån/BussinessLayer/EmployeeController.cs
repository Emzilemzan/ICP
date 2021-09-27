using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    /// <summary>
    /// Specific methods that handles Employees. 
    /// </summary>
    public class EmployeeController
    {
        public Employee GetEmployee(string username) => BusinessController.Instance.Context.Employees.Find(x => x.Username == username).FirstOrDefault();
        public IEnumerable<Employee> GetAllEmployees() => BusinessController.Instance.Context.Employees.GetAll();
        public bool ValidateEmployee(string username, string password)
        {
            if (GetEmployee(username)?.Password == password)
                BusinessController.Instance.CurrentEmployee = GetEmployee(username);
            return BusinessController.Instance.CurrentEmployee != null;
        }

        public void AddEmployee(Employee employee)
        {
            BusinessController.Instance.Context.Employees.Add(employee);
            BusinessController.Instance.Save();
        }

        public void RemoveEmployee(Employee employee)
        {
            BusinessController.Instance.Context.Employees.Remove(employee);
            BusinessController.Instance.Save();
        }
    }
}
