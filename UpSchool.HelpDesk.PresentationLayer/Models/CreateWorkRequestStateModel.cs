namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class CreateWorkRequestStateModel
    {
        public int WorkRequestId { get; set; }
        public string Description { get; set; } = null!;
    }
}
