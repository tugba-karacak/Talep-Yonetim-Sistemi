using System.ComponentModel.DataAnnotations;

namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Email gereklidir")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password gereklidir")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage ="Name gereklidir")]
        public string Name { get; set; } = null!;
    }
}
