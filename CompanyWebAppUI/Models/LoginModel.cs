using System.ComponentModel.DataAnnotations;

namespace CompanyWebAppUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please, provide your username")]
        public string? Id { get; set; }
        [Required(ErrorMessage = "Please, provide your password")]
        public string? Password { get; set; }
    }
}
