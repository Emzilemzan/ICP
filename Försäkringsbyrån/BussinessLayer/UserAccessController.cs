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
    /// Manages useraccess. 
    /// </summary>
    public class UserAccessController
    {
        public UserAccess GetUser(string username) => BusinessController.Instance.Context.Accesses.Find(x => x.Username == username).FirstOrDefault();
        public IEnumerable<UserAccess> GetAllUsers() => BusinessController.Instance.Context.Accesses.GetAll();
        public bool ValidateEmployee(string username, string password)
        {
            if (GetUser(username)?.Password == password)
                BusinessController.Instance.CurrentUser = GetUser(username);
            return BusinessController.Instance.CurrentUser != null;
        }

        public void CheckExistingUser(string id, UserAccess a)
        {
            UserAccess x = BusinessController.Instance.Context.Accesses.GetById(id);
            if (x == null)
            {

                var msg = $"Användarnamn: {a.Username} - Lösenord: {a.Password}";
                MessageBox.Show(msg, "Ny anställd att lägga till", MessageBoxButton.OK, MessageBoxImage.Information);
                AddUser(a);
            }
            else
            {
                MessageBox.Show("Går ej lägga till ny anställd då anställningsnumret redan finns");
            }
        }

        public void AddUser(UserAccess a)
        {
            BusinessController.Instance.Context.Accesses.Add(a);
            BusinessController.Instance.Save();
        }

        public void RemoveUser(UserAccess a)
        {
            BusinessController.Instance.Context.Accesses.Remove(a);
            BusinessController.Instance.Save();
        }

        public void EditUser(UserAccess a)
        {
            BusinessController.Instance.Context.Accesses.Update(a);
            BusinessController.Instance.Save();
        }
    }
}
