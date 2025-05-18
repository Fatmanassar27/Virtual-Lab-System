using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class ExperimentDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string PdfPath { get; set; }
    }
}
