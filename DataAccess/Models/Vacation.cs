namespace DataAccess.Models
{
    public class Vacation
    {
        public string? DocId { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Name { get; set; }
        public decimal? Hours { get; set; }
    }
}
