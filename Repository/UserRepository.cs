using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AdMedAPI.Data;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;
using AdMedAPI.Services;

namespace AdMedAPI.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username);

            // Return null if user not found
            if (user == null)
            {
                return true;
            }

            return false;
        }

        public bool DoPasswordsMatch(string password, string confirmPassword)
        {

            if (!password.Equals(confirmPassword))
            {
                return false;
            }

            return true;

        }

        public User GetUser(string username)
        {

            var user = _db.Users.FirstOrDefault(a => a.Username == username);

            if (user != null)
            {

                User userObj = new User()
                {
                    Id = user.Id,
                    Role = user.Role,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password
                };

                return userObj;

            }

            return null;

        }

        public User Authenticate(string username, string password)
        {

            if (_db.Users.Any(user => user.Username.Equals(username)))
            {

                User user = _db.Users.Where(u => u.Username.Equals(username)).First();

                // Calculate hash password from data of client and compare with hash in server with salt
                var client_post_hash_password = Convert.ToBase64String(RandomSalt.SaltHashPassword(
                    Encoding.ASCII.GetBytes(password),
                    Convert.FromBase64String(user.Salt)));

                if (client_post_hash_password.Equals(user.Password))
                {
                    // If the user was found, generate a JWT Token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddHours(12),
                        SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.Token = tokenHandler.WriteToken(token);
                    user.Password = "";
                    return user;
                }

            }

            return null;

        }

        public User Register(string username, string password, string firstname, string lastname)
        {

            if (_db.Users.Any(user => user.Username.Equals(username)))
            {
                return null;
            }

            User userObj = new User();

            userObj.Username = username; // Get the username
            userObj.FirstName = firstname; // Get the first name
            userObj.LastName = lastname; // Get the last name
            userObj.Salt = Convert.ToBase64String(RandomSalt.GetRandomSalt(16)); // Get random salt
            userObj.Password = Convert.ToBase64String(RandomSalt.SaltHashPassword(
                Encoding.ASCII.GetBytes(password),
                Convert.FromBase64String(userObj.Salt)));
            userObj.Role = "Resident";

            _db.Users.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;

        }

        public bool ResetPassword(string username, string password, string confirmpassword, string existingpassword)
        {

            if (_db.Users.Any(user => user.Username.Equals(username)))
            {

                User user = _db.Users.Where(u => u.Username.Equals(username)).First();

                // Calculate hash password from data of client and compare with hash in server with salt
                var client_post_hash_password = Convert.ToBase64String(RandomSalt.SaltHashPassword(
                    Encoding.ASCII.GetBytes(existingpassword),
                    Convert.FromBase64String(user.Salt)));

                if (client_post_hash_password.Equals(user.Password))
                {

                    user.Salt = Convert.ToBase64String(RandomSalt.GetRandomSalt(16)); // Get random salt
                    user.Password = Convert.ToBase64String(RandomSalt.SaltHashPassword(
                        Encoding.ASCII.GetBytes(password),
                        Convert.FromBase64String(user.Salt)));

                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return true;

                }

                return false;

            }

            return false;

        }

        public bool UpdateUser(User userToUpdate)
        {

            User user = _db.Users.Where(u => u.Id == userToUpdate.Id).First();

            user.Username = userToUpdate.Username;
            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.Role = userToUpdate.Role;

            _db.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}
