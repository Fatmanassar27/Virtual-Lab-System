using System.Diagnostics;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;

namespace Virtual_Lab_System.Repository
{
    public interface IReportRepository
    {
        Task<(int Total, List<Report> Reports)> GetAllReports(int page, int pageSize);
        Task<(int Total, List<Report> Reports)> GetReportsForExperiment(int experimentId , int page, int pageSize);
        Task<Report?> GetReportById(int id);
        Task<Report?> GradeReport(int reportId,float? grade);
        List<Report?> GetReportsByStudentUserName(string username);
        Task<Report?> CreateReport(string studentId, ReportDto dto);
        Task<bool> DeleteReport(int id);
    }

}
