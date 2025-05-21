using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int SubjectId { get; set; }

        public string TeacherId { get; set; }  // Foreign key

        // Navigation Properties
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }

        [ForeignKey(nameof(TeacherId))]
        public ApplicationUser Teacher { get; set; }

        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }

}
