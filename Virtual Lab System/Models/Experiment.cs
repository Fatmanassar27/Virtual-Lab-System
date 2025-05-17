using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.Models
{
    public class Experiment
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? PdfFileName { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}
