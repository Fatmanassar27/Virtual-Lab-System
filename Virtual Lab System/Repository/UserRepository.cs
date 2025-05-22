using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser? GetById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.Include(u => u.Subject).Include(u => u.Reports).ToList();
        }

        public IEnumerable<ApplicationUser> GetTeachers()
        {
            var users = _userManager.Users.Include(u => u.Subject).ToList();
            var teachers = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                if (roles.Contains("Teacher"))
                {
                    teachers.Add(user);
                }
            }

            return teachers;
        } 
        public IEnumerable<ApplicationUser> GetStudents()
        {
            var users = _userManager.Users.Include(u => u.Reports).ToList();
            var Students = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result;
                if (roles.Contains("Student"))
                {
                    Students.Add(user);
                }
            }

            return Students;
        }
    }

}

