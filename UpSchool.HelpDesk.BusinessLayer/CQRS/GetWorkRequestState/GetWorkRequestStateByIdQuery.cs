using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer.WorkRequestState;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestState
{
    public class GetWorkRequestStateByIdQuery : IRequest<Result<WorkRequestStateDto>>
    {
        public int Id { get; set; }

        public GetWorkRequestStateByIdQuery(int id)
        {
            Id = id;
        }
    }
}
