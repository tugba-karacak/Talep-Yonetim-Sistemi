using Microsoft.AspNetCore.Identity;

namespace UpSchool.HelpDesk.EntityLayer
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public ApplicationUser()
        {
            this.CreatedDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; set; }

        public string? Image { get; set; }

        public string? Name { get; set; }

        public List<WorkRequest>? WorkRequests { get; set; }
    }
}
