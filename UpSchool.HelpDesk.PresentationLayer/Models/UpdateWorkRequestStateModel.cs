namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class UpdateWorkRequestStateModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public int WorkRequestId { get; set; }
    }
}
