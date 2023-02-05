using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.HelpDesk.EntityLayer
{
    public class ApplicationRole : IdentityRole<int>, IEntity
    {
        public ApplicationRole()
        {
            this.CreatedDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; set; }
    }
}
