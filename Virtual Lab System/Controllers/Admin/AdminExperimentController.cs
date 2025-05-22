using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminExperimentController : ControllerBase
    {
        private readonly unitOfWork _unit;
        public AdminExperimentController(unitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet]
        public async Task<IActionResult> GetExperiments([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var experiments = await _unit.Experiment.GetAll(page, pageSize, search);
            var total = await _unit.Experiment.GetTotalCount(search);
            var result = _unit._mapper.Map<List<ExperimentDto>>(experiments);
            return Ok(new { Total = total, Experiments = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperiment(int id)
        {
            var exp = await _unit.Experiment.GetById(id);
            if (exp == null) return NotFound();
            var result = _unit._mapper.Map<ExperimentDto>(exp);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExperiment([FromBody] ExperimentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var exp = _unit._mapper.Map<Experiment>(dto);
            exp.Subject = _unit._db.Subjects.FirstOrDefault(s => s.Id == dto.SubjectId);
            exp.Teacher = _unit.User.GetById(dto.TeacherId);

            var created = await _unit.Experiment.Add(exp);
            return Ok(_unit._mapper.Map<ExperimentDto>(created));
        }
        [HttpPost("upload-pdf/{id}")]
        public async Task<IActionResult> UploadPdf(int id, IFormFile pdfFile)
        {
            var result = await _unit.Experiment.UploadPdf(id, pdfFile, _unit._env.WebRootPath);
            if (result == null) return BadRequest("Invalid PDF or experiment not found");
            return Ok(new { PdfFileName = result });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperiment(int id, [FromBody] ExperimentDto dto)
        {
            var updated = await _unit.Experiment.Update(id, new Experiment
            {
                Title = dto.Title,
                Description = dto.Description,
                PdfFileName = dto.PdfPath
            });

            if (updated == null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiment(int id)
        {
            var success = await _unit.Experiment.Delete(id);
            return success ? NoContent() : NotFound();
        }

    }
}
