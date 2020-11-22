namespace AdMedAPI.Dtos.Models
{
    // UserPasswordReset Dto
    public class UserPasswordResetDto
    {
        // User details
        public string Username { get; set; }
        public string ExistingPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}