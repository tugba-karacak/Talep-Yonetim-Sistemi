using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Configurations
{
    public class WorkRequestConfiguration : IEntityTypeConfiguration<WorkRequest>
    {
        public void Configure(EntityTypeBuilder<WorkRequest> builder)
        {

           

            builder.HasOne(x => x.AssignedUser).WithMany(x => x.WorkRequests).HasForeignKey(x => x.AssignedUserId);

            builder.Property(x => x.Title).HasMaxLength(300);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x=>x.State).HasMaxLength(200);
        }
    }
}
