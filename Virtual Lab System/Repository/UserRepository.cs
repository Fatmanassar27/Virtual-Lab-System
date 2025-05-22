using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public ApplicationUser? GetById(string id)
        {
            return _userManager.Users.Include(u => u.Subject).Include(u => u.Reports).FirstOrDefault(u => u.Id == id);
        }
        public ApplicationUser? GetByName(string name)
        {
            return _userManager.Users.Include(u => u.Subject).Include(u => u.Reports).FirstOrDefault(u => u.UserName == name);
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
        public async Task<ApplicationUser?> UpdateUser(string userName, UpdateUserDto dto)
        {
            var user =_userManager.Users.Include(u => u.Subject).Include(u => u.Reports).FirstOrDefault(u => u.UserName == userName);
            if (user == null) return null;

            _mapper.Map(dto, user);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? user : null;
        }
    }

}

