using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Repository;
using Microsoft.AspNetCore.Identity;

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
    public async Task<(int Total, List<Report> Reports)> GetReportsForExperiment(int experimentId ,int page, int pageSize)
    {
        var total = await _context.Reports.CountAsync();
        var reports = await _context.Reports
            .Include(r => r.Student)
            .Include(r => r.Experiment)
            .Where(r => r.ExperimentId == experimentId)
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
    public List<Report?> GetReportsByStudentUserName(string username)
    {
        var reports = _context.Reports
            .Include(r => r.Student)
            .Include(r => r.Experiment)
            .Where(r => r.Student.UserName == username).ToList();

        return reports;
    }

    public async Task<Report?> CreateReport(string studentId, ReportDto dto)
    {
        var student = _context.Users.Include(u => u.Subject).Include(u => u.Reports).FirstOrDefault(u => u.UserName == studentId);

        if (student == null) return null;

        var report = new Report
        {
            StudentId = student.Id,
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

    public async Task<Report?> GradeReport(int reportId, float? grade)
    {
        var report = await GetReportById(reportId);
        report.Grade = grade;
        _context.Reports.Update(report);
        _context.SaveChanges();
        return report;
    }
}
 