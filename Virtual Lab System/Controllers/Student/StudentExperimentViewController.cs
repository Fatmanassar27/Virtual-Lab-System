using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.Repository;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentExperimentViewController : ControllerBase
    {
        private readonly unitOfWork _unit;
        public StudentExperimentViewController(unitOfWork unit)
        {
            _unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentExperiments([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var experiments = await  _unit.Experiment.GetAll( page, pageSize ,search);
            foreach (var experiment in experiments)
            {
                experiment.Teacher = _unit.User.GetById(experiment.TeacherId);
            }
            var result = _unit._mapper.Map<List<ExperimentDto>>(experiments);
            foreach (var exp in result)
            {
                exp.TeacherName = _unit.User.GetById(exp.TeacherId).FullName;
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperimentById(int id)
        {
            var experiment = await _unit.Experiment.GetById(id);
            if (experiment == null) return NotFound();
            experiment.Teacher = _unit.User.GetById(experiment.TeacherId);
            var result = _unit._mapper.Map<ExperimentDto>(experiment);
            result.TeacherName = experiment.Teacher.FullName;
            return Ok(result);
        }
    }
}
