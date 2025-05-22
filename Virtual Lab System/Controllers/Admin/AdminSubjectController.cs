using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.Repository;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminSubjectController : ControllerBase
    {
        unitOfWork _unit;
        public AdminSubjectController(unitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _unit.Subject.GetAllAsync();
            var results = _unit._mapper.Map<List<SubjectDetailsDto>>(subjects);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _unit.Subject.GetByIdAsync(id);
            if (subject == null)
                return NotFound();
            var result = _unit._mapper.Map<SubjectDetailsDto>(subject);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDTO subject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var s = _unit._mapper.Map<Subject>(subject);
            await _unit.Subject.AddAsync(s);
            return Ok(s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDetailsDto subject)
        {
            if (id != subject.Id)
                return BadRequest();

            if (!await _unit.Subject.ExistsAsync(id))
                return NotFound();

            await _unit.Subject.UpdateAsync(subject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _unit.Subject.GetByIdAsync(id);
            if (subject == null)
                return NotFound();

            await _unit.Subject.DeleteAsync(subject);
            return NoContent();
        }
    }
}
