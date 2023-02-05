using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Defaults;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetStatics
{
    public class GetStaticsQueryHandler : IRequestHandler<GetStaticsQuery, Result<StaticsDataDto>>
    {
        private readonly IUow uow;

        public GetStaticsQueryHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<StaticsDataDto>> Handle(GetStaticsQuery request, CancellationToken cancellationToken)
        {
            var dto = new StaticsDataDto();

           dto.WorkRequestStateCount= this.uow.GetRepository<WorkRequestState>().GetQueryable().Count();
            dto.AccountCount = this.uow.GetRepository<ApplicationUser>().GetQueryable().Count();
            dto.WorkRequestCompletedCount = this.uow.GetRepository<WorkRequest>().GetQueryable().Count(x => x.State == WorkRequestStateDefaults.Completed);
            dto.WorkRequestCount = this.uow.GetRepository<WorkRequest>().GetQueryable().Count();

            return new Result<StaticsDataDto>(dto);
        }
    }
}
