using AdMedAPI.Models;
using System.Collections.Generic;

namespace AdMedAPI.Repository.IRepository
{
    // User repository interface
    public interface IUserRepository
    {
        // All methods implemented in the repository
        bool IsUniqueUser(string username);
        bool ResetPassword(string username, string password, string confirmpassword, string existingpassword);
        bool AdminResetPassword(string username, string password, string confirmpassword);
        bool DoPasswordsMatch(string password, string confirmPassword);
        bool UpdateUser(User user);
        ICollection<User> GetUsers();
        User GetUser(int id);
        User Authenticate(string username, string password);
        User Register(string username, string password, string firstname, string lastname);
        bool Save();
    }
}