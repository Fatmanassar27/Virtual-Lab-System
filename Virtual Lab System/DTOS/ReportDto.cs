using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class ReportDto
    {
        [Required]
        public int ExperimentId { get; set; }
        [Required]
        public string Results { get; set; }
        [Required]
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string ExperimentTitle { get; set; }
        public DateTime SubmissionDate { get; set; }
        public float? Grade { get; set; }
        public string? Comments { get; set; }
    }
}
