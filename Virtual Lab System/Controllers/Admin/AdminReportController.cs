using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminReportController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public AdminReportController(unitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (total, reports) = await _unit.Report.GetAllReports(page, pageSize);
            var reportDtos = _unit._mapper.Map<List<ReportDto>>(reports);

            return Ok(new
            {
                Total = total,
                Data = reportDtos,
                Page = page,
                PageSize = pageSize
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _unit.Report.GetReportById(id);
            if (report == null)
                return NotFound("Report not found");
            var result = _unit._mapper.Map<ReportDto>(report);
            return Ok(result);
        }
    }
}
