namespace DataAccess.Models
{
    public class SickLeave
    {
        public string? DocId { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Name { get; set; }
    }
}
