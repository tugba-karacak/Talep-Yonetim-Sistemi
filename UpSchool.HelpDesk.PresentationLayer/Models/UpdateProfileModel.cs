namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class UpdateProfileModel
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }

        public IFormFile File { get; set; }
    }
}
