//using Microsoft.AspNetCore.Authorization;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Virtual_Lab_System.Models;
//using Virtual_Lab_System.DTOS;
//using Virtual_Lab_System.Repository;

//namespace Virtual_Lab_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExperimentController : ControllerBase
//    {
//        private readonly IExperimentRepository _repo;
//        private readonly IWebHostEnvironment _env;

//        public ExperimentController(IExperimentRepository repo, IWebHostEnvironment env)
//        {
//            _repo = repo;
//            _env = env;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetExperiments([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
//        {
//            var experiments = await _repo.GetAll(page, pageSize, search);
//            var total = await _repo.GetTotalCount(search);
//            return Ok(new { Total = total, Experiments = experiments });
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetExperiment(int id)
//        {
//            var exp = await _repo.GetById(id);
//            if (exp == null) return NotFound();
//            return Ok(exp);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateExperiment([FromBody] ExperimentDto dto)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);

//            var exp = new Experiment { Title = dto.Title, Description = dto.Description, PdfFileName = dto.PdfPath };
//            var created = await _repo.Add(exp);
//            return CreatedAtAction(nameof(GetExperiment), new { id = created.Id }, created);
//        }

//        [HttpPost("upload-pdf/{id}")]
//        public async Task<IActionResult> UploadPdf(int id, IFormFile pdfFile)
//        {
//            var result = await _repo.UploadPdf(id, pdfFile, _env.WebRootPath);
//            if (result == null) return BadRequest("Invalid PDF or experiment not found");
//            return Ok(new { PdfFileName = result });
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateExperiment(int id, [FromBody] ExperimentDto dto)
//        {
//            var updated = await _repo.Update(id, new Experiment
//            {
//                Title = dto.Title,
//                Description = dto.Description,
//                PdfFileName = dto.PdfPath
//            });

//            if (updated == null) return NotFound();
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteExperiment(int id)
//        {
//            var success = await _repo.Delete(id);
//            return success ? NoContent() : NotFound();
//        }

//    }
//}

