using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.HelpDesk.DtoLayer.WorkRequestState
{
    public class WorkRequestStateDto
    {
        public int Id { get; set; }

        public int WorkRequestId { get; set; }
        public string Description { get; set; } = null!;
    }
}
