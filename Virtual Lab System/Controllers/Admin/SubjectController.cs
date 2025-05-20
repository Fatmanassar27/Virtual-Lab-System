using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Models;
using Virtual_Lab_System.Repository;

namespace Virtual_Lab_System.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repo;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectRepository repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _repo.GetAllAsync();
            var results = _mapper.Map<List<SubjectDetailsDto>>(subjects);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _repo.GetByIdAsync(id);
            if (subject == null)
                return NotFound();
            var result = _mapper.Map<SubjectDetailsDto>(subject);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectDTO subject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var s = _mapper.Map<Subject>(subject);
            await _repo.AddAsync(s);
            return Ok(s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, [FromBody] SubjectDetailsDto subject)
        {
            if (id != subject.Id)
                return BadRequest();

            if (!await _repo.ExistsAsync(id))
                return NotFound();

            await _repo.UpdateAsync(subject);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _repo.GetByIdAsync(id);
            if (subject == null)
                return NotFound();

            await _repo.DeleteAsync(subject);
            return NoContent();
        }
    }
}
