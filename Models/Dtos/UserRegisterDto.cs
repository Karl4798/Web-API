using AdMedAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdMedAPI.Dtos.Models
{
    // UserRegister Dto
    public class UserRegisterDto
    {
        // User details
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}