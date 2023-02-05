namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class WorkRequestStateModel
    {
        public int Id { get; set; }

        public int WorkRequestId { get; set; }
        public string Description { get; set; } = null!;
    }
}
