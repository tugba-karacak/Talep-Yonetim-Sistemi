using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer.Statics;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetStatics
{
    public class GetStaticForGraphQuery : IRequest<Result<List<StaticsForGraphDto>>>
    {
    }
}
