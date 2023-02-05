using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer.WorkRequestState;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestState
{
    public class GetWorkRequestStateByIdQueryHandler : IRequestHandler<GetWorkRequestStateByIdQuery, Result<WorkRequestStateDto>>
    {
        private readonly IUow uow;
        private readonly IMapper mapper;

        public GetWorkRequestStateByIdQueryHandler(IUow uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Result<WorkRequestStateDto>> Handle(GetWorkRequestStateByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await this.uow.GetRepository<WorkRequestState>().GetByFilterAsync(x => x.Id == request.Id);
           var mappedData = this.mapper.Map<WorkRequestStateDto>(data);

            return new Result<WorkRequestStateDto>(mappedData);
        }
    }
}
