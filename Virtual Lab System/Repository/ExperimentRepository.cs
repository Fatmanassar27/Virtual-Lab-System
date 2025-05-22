using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.Repository;

public class ExperimentRepository : IExperimentRepository
{
    private readonly Context _context;

    public ExperimentRepository(Context context)
    {
        _context = context;
    }

    public async Task<List<Experiment>> GetAll(int page, int pageSize, string search)
    {
        var query = _context.Experiments.Include(e => e.Subject).Include(e => e.Teacher).AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(e => e.Title.ToLower().Contains(search.ToLower()) || e.Description.ToLower().Contains(search.ToLower()));

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetTotalCount(string search)
    {
        var query = _context.Experiments.Include(e => e.Subject).Include(e => e.Teacher).AsQueryable();
        if (!string.IsNullOrEmpty(search))
            query = query.Where(e => e.Title.Contains(search) || e.Description.Contains(search));

        return await query.CountAsync();
    }

    public async Task<Experiment?> GetById(int id)
    {
        return await _context.Experiments.Include(e => e.Subject).Include(e => e.Teacher).FirstOrDefaultAsync(e => e.Id == id); 
    }

    public async Task<Experiment> Add(Experiment experiment)
    {
        _context.Experiments.Add(experiment);
        await _context.SaveChangesAsync();
        return experiment;
    }

    public async Task<Experiment?> Update(int id, Experiment updated)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null) return null;

        experiment.Title = updated.Title;
        experiment.Description = updated.Description;
        experiment.PdfFileName = updated.PdfFileName;
        await _context.SaveChangesAsync();

        return experiment;
    }

    public async Task<bool> Delete(int id)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null) return false;

        _context.Experiments.Remove(experiment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> UploadPdf(int id, IFormFile file, string webRootPath)
    {
        var experiment = await _context.Experiments.FindAsync(id);
        if (experiment == null || file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
            return null;

        var uploadsFolder = Path.Combine(webRootPath, "Uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, $"{id}_{file.FileName}");
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        experiment.PdfFileName = $"/Uploads/{id}_{file.FileName}";
        await _context.SaveChangesAsync();

        return experiment.PdfFileName;
    }

    public async Task<List<Experiment>> GetExperimentsByTeacher(string userId)
    {
        var query = await _context.Experiments.Include(e => e.Subject).Include(e => e.Teacher).Where(e => e.TeacherId == userId).ToListAsync();
        return query;
    }
}
