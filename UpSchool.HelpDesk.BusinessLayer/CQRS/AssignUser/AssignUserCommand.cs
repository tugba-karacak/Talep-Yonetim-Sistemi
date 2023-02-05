using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.AssignUser
{
    public class AssignUserCommand : IRequest<Result<NoContent>>
    {
        public int WorkRequestId { get; set; }
        public int UserId { get; set; }
    }
}
