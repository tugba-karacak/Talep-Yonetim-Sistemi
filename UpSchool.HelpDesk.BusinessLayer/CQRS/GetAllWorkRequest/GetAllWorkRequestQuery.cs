using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetAllWorkRequest
{
    public class GetAllWorkRequestQuery : IRequest<Result<List<WorkRequestDto>>>
    {
    }
}
