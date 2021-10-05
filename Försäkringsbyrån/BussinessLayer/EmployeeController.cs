using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public void CheckExistingEmployee(int id, Employee employee)
        {
            Employee e = BusinessController.Instance.Context.Employees.GetById(id);
            if (e == null)
            {
                
                    var msg = $"Anställningsnummer: {employee.EmploymentNo} - Efternamn: {employee.Lastname} - Förnamn: {employee.Firstname}";
                    MessageBox.Show(msg, "Ny anställd att lägga till", MessageBoxButton.OK, MessageBoxImage.Information);
                    AddEmployee(employee);
            }
            else
            {
                MessageBox.Show("Går ej lägga till ny anställd då anställningsnumret redan finns");
            }
        }

        public Access GetAccess(string id) => BusinessController.Instance.Context.Accesses.Find(x => x.EmployeeId == id).FirstOrDefault();


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

        public void Edit(Employee employee)
        {
            Employee edit = BusinessController.Instance.Context.Employees.GetById(employee.EmploymentNo);
            edit.Lastname = employee.Lastname;
            edit.Firstname = employee.Firstname;
            edit.TaxRate = employee.TaxRate;
            edit.StreetAddress = employee.StreetAddress;
            edit.City = employee.City; 
            edit.Postalcode = employee.Postalcode;
            edit.FormOfEmployment = employee.FormOfEmployment;
            edit.Password = employee.Password;
            edit.Username = employee.Username;
            //edit.Accesses = employee.Accesses;
            //edit.Roles = employee.Roles;
            BusinessController.Instance.Save();
        }


    }


}
