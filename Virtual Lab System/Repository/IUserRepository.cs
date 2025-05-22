using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public interface IUserRepository
    {
        ApplicationUser? GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> GetTeachers();
        IEnumerable<ApplicationUser> GetStudents();
        Task<ApplicationUser?> UpdateUser(string userName, UpdateUserDto dto);
        ApplicationUser? GetByName(string name);
    }

}
