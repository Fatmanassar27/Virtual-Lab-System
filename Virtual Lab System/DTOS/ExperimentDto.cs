using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class ExperimentDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? PdfPath { get; set; }
        public int SubjectId { get; set; }
        public string? TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? SubjectName { get; set; }
    }
}
