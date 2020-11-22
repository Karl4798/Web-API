namespace AdMedAPI.Dtos.Models
{
    // AdminPasswordReset Dto
    public class AdminPasswordResetDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}