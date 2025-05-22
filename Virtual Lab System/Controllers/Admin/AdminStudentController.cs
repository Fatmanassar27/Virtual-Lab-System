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
    [AllowAnonymous]
    public class AdminStudentController : ControllerBase
    {
        private readonly unitOfWork _unit;

        public AdminStudentController(unitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _unit.User.GetStudents();
            var result = _unit._mapper.Map<List<StudentDto>>(students);
            foreach (var student in result)
            {
                student.ReportsTitles = _unit._db.Reports.Where(r => r.StudentId == student.Id).Select(r => r.Experiment.Title).ToList();
            }
            return Ok(result);
        }
    }
}
