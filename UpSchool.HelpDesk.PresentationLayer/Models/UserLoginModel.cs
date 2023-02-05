using System.ComponentModel.DataAnnotations;

namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email gereklidir")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password gereklidir")]
        public string Password { get; set; } = null!;
    }
}
