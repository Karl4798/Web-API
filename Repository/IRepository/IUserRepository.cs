using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{

    public interface IUserRepository
    {

        bool IsUniqueUser(string username);
        bool DoPasswordsMatch(string password, string confirmPassword);
        User Authenticate(string username, string password);
        User Register(string username, string password, string firstname, string lastname);

    }

}
