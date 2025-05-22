using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public interface IUserRepository
    {
        ApplicationUser? GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> GetTeachers(); // لو فيه أدوار
    }

}
