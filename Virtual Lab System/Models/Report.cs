using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser Student { get; set; }

        public int ExperimentId { get; set; }
        [ForeignKey("ExperimentId")]
        public Experiment Experiment { get; set; }

        public string ResultData { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
