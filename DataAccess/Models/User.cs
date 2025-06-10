namespace DataAccess.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? WorkerId { get; set; }
        public string? PasswordHash { get; set; }
        public int? Temp { get; set; }
    }
}
