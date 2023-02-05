using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequest
{
    public class GetWorkRequestByIdQueryHandler : IRequestHandler<GetWorkRequestByIdQuery, Result<WorkRequestDto>>
    {
        private readonly IUow uow;
        private readonly IMapper mapper; 

        public GetWorkRequestByIdQueryHandler(IUow uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Result<WorkRequestDto>> Handle(GetWorkRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await this.uow.GetRepository<WorkRequest>().GetByFilterAsync(x => x.Id == request.Id);
            var mappedData = this.mapper.Map<WorkRequestDto>(data);
            return new Result<WorkRequestDto>(mappedData);
        }
    }
}
