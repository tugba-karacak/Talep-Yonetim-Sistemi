namespace UpSchool.HelpDesk.EntityLayer
{
    public class WorkRequest : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int? AssignedUserId { get; set; }

        public string State { get; set; } = null!;

        public ApplicationUser? AssignedUser { get; set; }

        public List<WorkRequestState>? WorkRequestStates { get; set; }
    }
}
