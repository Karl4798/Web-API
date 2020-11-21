using AdMedAPI.Models;
using System.Collections.Generic;

namespace AdMedAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        bool ResetPassword(string username, string password, string confirmpassword, string existingpassword);
        bool DoPasswordsMatch(string password, string confirmPassword);
        bool UpdateUser(User user);
        ICollection<User> GetUsers();
        User GetUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password, string firstname, string lastname);
        bool Save();
    }
}