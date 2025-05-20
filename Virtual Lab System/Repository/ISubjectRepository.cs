
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public interface ISubjectRepository : IRepository
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task AddAsync(Subject subject);
        Task UpdateAsync(SubjectDetailsDto subject);
        Task DeleteAsync(Subject subject);
        Task<bool> ExistsAsync(int id);
        Task<Subject?> GetByNameAsync(string Name);

    }

}

