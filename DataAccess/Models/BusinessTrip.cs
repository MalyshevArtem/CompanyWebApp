namespace DataAccess.Models
{
    public class BusinessTrip
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DocDate { get; set; }
        public string? DocId { get; set; }
        public string? Destination { get; set; }
        public string? Description { get; set; }
    }
}
