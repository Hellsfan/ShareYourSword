﻿namespace ShareYourSword.Models
{
    public class LoginViewModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Message { get; internal set; }
    }
}
