using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Configurations
{
    public class WorkRequestStateConfiguration : IEntityTypeConfiguration<WorkRequestState>
    {
        public void Configure(EntityTypeBuilder<WorkRequestState> builder)
        {
            builder.ToTable(tb => tb.HasTrigger("WorkStateAddedTrigger"));
            builder.HasOne(x => x.WorkRequest).WithMany(x => x.WorkRequestStates).HasForeignKey(x => x.WorkRequestId);
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired(true);
           
        }
    }
}
