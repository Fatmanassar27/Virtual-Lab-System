namespace Virtual_Lab_System.DTOS
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string>? ReportsTitles { get; set; } = new List<string>(); // لو عايزة تعرضي التقارير بتاعته
    }
}
