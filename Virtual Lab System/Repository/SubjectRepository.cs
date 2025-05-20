using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly Context _context;

        public SubjectRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Subject subject)
        {
            var s = _context.Subjects.FirstOrDefault(s => s.Name == subject.Name);
            if (subject != null && s == null)
            {
                await _context.Subjects.AddAsync(subject);
                await SaveChanges();
            }
        }

        public async Task DeleteAsync(Subject subject)
        {
            var s = _context.Subjects.FirstOrDefault(s => s.Name == subject.Name);
            if (subject != null && s != null)
            {
                _context.Subjects.Remove(subject);
                await SaveChanges();
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Subjects.AnyAsync(s => s.Id == id);
        }

        public Task<List<Subject>> GetAllAsync()
        {
            return _context.Subjects.Include(s => s.Teachers).Include(s => s.Experiments).ToListAsync();
        }

        public Task<Subject?> GetByIdAsync(int id)
        {
            return _context.Subjects.Include(s => s.Teachers).Include(s => s.Experiments).FirstOrDefaultAsync(s => s.Id == id);

        }
        public Task<Subject?> GetByNameAsync(string Name)
        {
            return _context.Subjects.Include(s => s.Teachers).Include(s => s.Experiments).FirstOrDefaultAsync(s => s.Name == Name);

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubjectDetailsDto subject)
        {
            var s = _context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
            if (subject != null && s != null)
            {
                s.Name = subject.Name;
                _context.Subjects.Update(s);
                await SaveChanges();
            }
        }
    }
}
