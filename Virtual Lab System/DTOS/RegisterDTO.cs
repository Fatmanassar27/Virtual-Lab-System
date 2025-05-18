using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.DTOS
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } // Admin | Teacher | Student
    }

}
