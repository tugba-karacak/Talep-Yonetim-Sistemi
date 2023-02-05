using System.ComponentModel.DataAnnotations;

namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class UpdateWorkRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Açıklama gereklidir")]
        public string Description { get; set; } = null!;
    }
}
