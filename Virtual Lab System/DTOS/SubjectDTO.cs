using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.DTOS
{
    public class SubjectDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
