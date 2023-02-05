namespace UpSchool.HelpDesk.EntityLayer
{
    public class WorkRequestState : IEntity
    {
        public int Id { get; set; }

        public int WorkRequestId { get; set; }

        public WorkRequest? WorkRequest { get; set; }
        public string Description { get; set; } = null!;

    }
}
