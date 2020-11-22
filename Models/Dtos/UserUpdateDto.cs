using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdMedAPI.Models.Dtos
{
    // UserUpdate Dto
    public class UserUpdateDto
    {
        // User details
        public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Role { get; set; }
    }
}