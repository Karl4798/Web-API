using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Dtos.Models
{
    // UserAuthentication Dto
    public class UserAuthenticationDto
    {
        // User details
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}