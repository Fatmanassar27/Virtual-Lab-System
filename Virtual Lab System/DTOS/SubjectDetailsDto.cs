namespace Virtual_Lab_System.DTOS
{
    public class SubjectDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<string>? Teachers { get; set; }
        public List<string>? Experiments { get; set; }
    }
}
