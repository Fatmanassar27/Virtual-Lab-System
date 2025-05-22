using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? ProfilePictureUrl { get; set; }

    }
}
