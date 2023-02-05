using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool.HelpDesk.DtoLayer
{
    public class StaticsDataDto
    {
        public int AccountCount { get; set; }
        public int WorkRequestCount { get; set; }
        public int WorkRequestStateCount { get; set; }

        public int WorkRequestCompletedCount { get; set; }
    }
}
