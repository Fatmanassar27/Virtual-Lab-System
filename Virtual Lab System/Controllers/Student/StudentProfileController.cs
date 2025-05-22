using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Virtual_Lab_System.DTOS;
using Virtual_Lab_System.UnitOfWork;

namespace Virtual_Lab_System.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student,Teacher,Admin")]
    public class StudentProfileController : ControllerBase
    {
        private readonly unitOfWork _unit;
        public StudentProfileController(unitOfWork unit)
        {
            _unit = unit;
        }


        [HttpGet]
        public IActionResult GetStudentProfile()
        {

            var userId = User.Identity?.Name;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = _unit.User.GetByName(userId);
            if (user == null)
                return NotFound();

            var userDto = new UpdateUserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfileImage
            };

            return Ok(userDto);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentProfile([FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _unit.User.UpdateUser(User.Identity!.Name!, dto);
            if (result == null)
            {
                return BadRequest("Update failed");
            }
            else
            {
                return NoContent();
            }

        }
    }
}