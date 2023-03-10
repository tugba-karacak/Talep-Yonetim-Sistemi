namespace UpSchool.HelpDesk.PresentationLayer.Models
{
    public class WorkRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int? AssignedUserId { get; set; }

        public string State { get; set; } = null!;
    }
}
