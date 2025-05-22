using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherReportReviewController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public TeacherReportReviewController(unitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet("experiment/{experimentId}")]
        public async Task<IActionResult> GetReportsForExperiment(int experimentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (total, reports) = await _unit.Report.GetReportsForExperiment(experimentId, page, pageSize);
            var result = _unit._mapper.Map<List<ReportDto>>(reports);
            return Ok(new { Total = total, Reports = result });
        }

        [HttpPut("grade/{reportId}")]
        public async Task<IActionResult> GradeReport(int reportId, [FromBody] int grade)
        {
            var result = await _unit.Report.GradeReport(reportId, grade);
            if (result == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(_unit._mapper.Map<ReportDto>(result));
            }
        }
    }
}
