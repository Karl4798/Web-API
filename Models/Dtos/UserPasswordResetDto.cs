﻿namespace AdMedAPI.Dtos.Models
{
    public class UserPasswordResetDto
    {
        public string Username { get; set; }
        public string ExistingPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}