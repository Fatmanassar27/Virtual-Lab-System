using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Virtual_Lab_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        public string? ProfileImage { get; set; }
        public ICollection<Report>? Reports { get; set; }

    }
}
