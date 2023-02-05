using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer.WorkRequestState;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestStateByWorkRequestId
{
    public class GetWorkStatesByWorkRequestIdQueryHandler : IRequestHandler<GetWorkStatesByWorkRequestIdQuery, Result<List<WorkRequestStateDto>>>
    {
        private readonly IUow uow;
        private readonly IMapper mapper;

        public GetWorkStatesByWorkRequestIdQueryHandler(IUow uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Result<List<WorkRequestStateDto>>> Handle(GetWorkStatesByWorkRequestIdQuery request, CancellationToken cancellationToken)
        {
            var data = await this.uow.GetRepository<WorkRequestState>().GetQueryable().Where(x => x.WorkRequestId == request.WorkRequestId).ToListAsync();

            var mappedData = this.mapper.Map<List<WorkRequestStateDto>>(data);

            return new Result<List<WorkRequestStateDto>>(mappedData);
        }
    }
}
