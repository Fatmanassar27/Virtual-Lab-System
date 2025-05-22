namespace Virtual_Lab_System.DTOS
{
    public class TeacherDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }

        public List<string>? Experments { get; set; } = new List<string>();
    }
}
