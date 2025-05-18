using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Repository;

public class ReportRepository : IReportRepository
{
    private readonly Context _context;

    public ReportRepository(Context context)
    {
        _context = context;
    }

    public async Task<(int Total, List<Report> Reports)> GetAllReports(int page, int pageSize)
    {
        var total = await _context.Reports.CountAsync();
        var reports = await _context.Reports
            .Include(r => r.Student)
            .Include(r => r.Experiment)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (total, reports);
    }

    public async Task<Report?> GetReportById(int id)
    {
        return await _context.Reports
            .Include(r => r.Student)
            .Include(r => r.Experiment)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Report?> CreateReport(string studentId, ReportDto dto)
    {
        var student = await _context.Users.FindAsync(studentId);
        if (student == null) return null;

        var report = new Report
        {
            StudentId = studentId,
            ExperimentId = dto.ExperimentId,
            ResultData = dto.Results,
            SubmissionDate = DateTime.UtcNow
        };

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        return report;
    }

    public async Task<bool> DeleteReport(int id)
    {
        var report = await _context.Reports.FindAsync(id);
        if (report == null) return false;

        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
        return true;
    }
}
