using Microsoft.AspNetCore.Identity;

namespace UpSchool.HelpDesk.EntityLayer
{
    public class ApplicationUserToken : IdentityUserToken<int>, IEntity
    {
        public DateTime ExpireDate { get; set; }    
    }
}
