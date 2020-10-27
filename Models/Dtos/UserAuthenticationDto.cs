using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Dtos.Models
{
    public class UserAuthenticationDto
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}