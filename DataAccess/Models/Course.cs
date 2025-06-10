namespace DataAccess.Models
{
    public class Course
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
