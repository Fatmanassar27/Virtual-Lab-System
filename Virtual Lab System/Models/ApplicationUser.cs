using Microsoft.AspNetCore.Identity;

namespace Virtual_Lab_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImage { get; set; }
        
    }
}
