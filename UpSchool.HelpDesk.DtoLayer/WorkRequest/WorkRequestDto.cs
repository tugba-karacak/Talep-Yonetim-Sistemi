using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.HelpDesk.DtoLayer
{
    public class WorkRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int? AssignedUserId { get; set; }

        public string State { get; set; } = null!;

    }
}
