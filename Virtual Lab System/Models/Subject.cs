using System.ComponentModel.DataAnnotations;

namespace Virtual_Lab_System.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<ApplicationUser>? Teachers { get; set; }
        public ICollection<Experiment>? Experiments { get; set; }


    }
}
