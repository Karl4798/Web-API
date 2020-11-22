using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdMedAPI.Models
{
    // User entity
    public class User
    {
        // User fields stored in the database
        [Key] public int Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        [NotMapped] public string Token { get; set; }
    }
}