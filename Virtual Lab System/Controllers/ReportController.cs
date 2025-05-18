using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Repository;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IReportRepository _repo;

    public ReportController(IReportRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Teacher")]
    public async Task<IActionResult> GetReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (total, reports) = await _repo.GetAllReports(page, pageSize);
        return Ok(new { Total = total, Reports = reports });
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Teacher,Student")]
    public async Task<IActionResult> GetReport(int id)
    {
        var report = await _repo.GetReportById(id);
        if (report == null) return NotFound();

        // Ensure student can only view their own reports
        if (User.IsInRole("Student") && report.StudentId != User.Identity.Name)
            return Forbid();

        return Ok(report);
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> CreateReport([FromBody] ReportDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var report = await _repo.CreateReport(User.Identity.Name!, dto);
        if (report == null) return BadRequest("Student not found");

        return CreatedAtAction(nameof(GetReport), new { id = report.Id }, report);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteReport(int id)
    {
        var deleted = await _repo.DeleteReport(id);
        return deleted ? NoContent() : NotFound();
    }
}
