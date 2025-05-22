using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class ReportOfStudentDto
    {
        [Required]
        public int ExperimentId { get; set; }
        [Required]
        public string Results { get; set; }
        public string ExperimentTitle { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
