using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class ReportDto
    {
        [Required]
        public int ExperimentId { get; set; }
        [Required]
        public string Results { get; set; }
    }
}
