using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminTeacherController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public AdminTeacherController (unitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = _unit.User.GetTeachers();
            var result = _unit._mapper.Map<List<TeacherDto>>(teachers);
            foreach (var teacher in result)
            {
                teacher.Experments = _unit._db.Experiments.Where(e => e.TeacherId == teacher.Id).Select(e => e.Title).ToList();
            }
            return Ok(result);
        }
        [HttpPut("AssignTeacher")]
        public async Task<IActionResult> AssignTeacherToExperiment(int experimentId, string teacherId)
        {
            var experiment = await _unit.Experiment.GetById(experimentId);
            if (experiment == null)
                return NotFound("Experiment not found");

            var teacher = _unit._db.Users.FirstOrDefault(u => u.Id == teacherId && _unit._db.UserRoles
                .Any(r => r.UserId == u.Id && r.RoleId == _unit._db.Roles.FirstOrDefault(role => role.Name == "Teacher").Id));

            if (teacher == null)
                return NotFound("Teacher not found or not assigned Teacher role");

            experiment.TeacherId = teacherId;
            experiment.Teacher = teacher;

            _unit.Save();

            return Ok("Teacher assigned successfully");
        }

    }
}
