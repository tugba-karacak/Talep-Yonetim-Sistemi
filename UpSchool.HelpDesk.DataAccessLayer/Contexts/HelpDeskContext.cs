using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using UpSchool.HelpDesk.DataAccessLayer.Configurations;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Contexts
{
    public class HelpDeskContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            builder.ApplyConfiguration(new WorkRequestConfiguration());
            builder.ApplyConfiguration(new WorkRequestStateConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<WorkRequest> WorkRequests { get; set; }
        public DbSet<WorkRequestState> WorkRequestStates { get; set; }

        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }
    }
}
