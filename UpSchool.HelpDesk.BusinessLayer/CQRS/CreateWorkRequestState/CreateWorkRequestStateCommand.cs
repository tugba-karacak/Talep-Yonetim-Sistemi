using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer.WorkRequestState;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState
{
    public class CreateWorkRequestStateCommand : IRequest<Result<WorkRequestStateDto>>
    {
        public int WorkRequestId { get; set; }
        public string Description { get; set; } = null!;
    }
}
