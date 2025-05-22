using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherExperimentController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public TeacherExperimentController(unitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyExperiments()
        {
            var username = User.Identity!.Name!;
            var userId = _unit.User.GetByName(username).Id;
            var experiments = await _unit.Experiment.GetExperimentsByTeacher(userId);
            var reult = _unit._mapper.Map<List<ExperimentDto>>(experiments);
            return Ok(reult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperiment([FromBody] ExperimentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var exp = _unit._mapper.Map<Experiment>(dto);
            exp.Subject = _unit._db.Subjects.FirstOrDefault(s => s.Id == dto.SubjectId);
            exp.Teacher = _unit.User.GetById(_unit.User.GetByName(User.Identity!.Name!).Id);
            
            var created = await _unit.Experiment.Add(exp);
            var result = _unit._mapper.Map<ExperimentDto>(created);
            result.TeacherName = created.Teacher.FullName;
            return Ok(result);
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
        [HttpPost("upload-pdf/{id}")]
        public async Task<IActionResult> UploadPdf(int id, IFormFile pdfFile)
        {
            var result = await _unit.Experiment.UploadPdf(id, pdfFile, _unit._env.WebRootPath);
            if (result == null) return BadRequest("Invalid PDF or experiment not found");
            return Ok(new { PdfFileName = result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperiment(int id)
        {
            var success = await _unit.Experiment.Delete(id);
            return success ? NoContent() : NotFound();
        }
    }
}
