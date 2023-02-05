using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequest
{
    public class GetWorkRequestByIdQuery : IRequest<Result<WorkRequestDto>>
    {
        public int Id { get; set; }

        public GetWorkRequestByIdQuery(int id)
        {
            Id = id;
        }
    }
}
