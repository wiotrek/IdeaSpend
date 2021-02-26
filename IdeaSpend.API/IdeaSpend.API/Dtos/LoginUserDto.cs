using System;

namespace IdeaSpend.API
{
    /// <summary>
    /// Information only about personal data user
    /// </summary>
    public class LoginUserDto
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }
        
        public DateTime Created { get; set; }
        
        public DateTime LastLogin { get; set; }

        public double Income { get; set; }
        
        public string Token { get; set; }
    }
}