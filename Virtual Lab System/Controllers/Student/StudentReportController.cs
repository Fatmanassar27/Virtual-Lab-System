using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentReportController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public StudentReportController(unitOfWork unit)
        {
            _unit = unit;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateReport([FromBody] ReportDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            dto.ExperimentTitle = _unit._db.Experiments.FirstOrDefault(e => e.Id == dto.ExperimentId).Title;
            var report = await _unit.Report.CreateReport(User.Identity.Name!, dto);
            if (report == null) return BadRequest("Student not found");

            return Ok(_unit._mapper.Map<ReportDto>(report));
        }

        [HttpGet("my")]
        public IActionResult GetMyReports()
        {
            string studentUserName = User.Identity.Name!;

            var reports = _unit.Report.GetReportsByStudentUserName(studentUserName);
            var result = _unit._mapper.Map<List<ReportOfStudentDto>>(reports);
            return Ok(result);
        }
    }
}
