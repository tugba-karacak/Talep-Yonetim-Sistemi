using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetWorkRequestByUserId
{
    public class GetWorkRequestByUserIdQueryHandler : IRequestHandler<GetWorkRequestByUserIdQuery, Result<List<WorkRequestDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public GetWorkRequestByUserIdQueryHandler(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<List<WorkRequestDto>>> Handle(GetWorkRequestByUserIdQuery request, CancellationToken cancellationToken)
        {
            var mappedData = _mapper.Map<List<WorkRequestDto>>(await _uow.GetRepository<WorkRequest>().GetQueryable().Where(x=>x.AssignedUserId==request.UserId && x.State == WorkRequestStateDefaults.Working).ToListAsync());

            return new Result<List<WorkRequestDto>>(mappedData);
        }
    }
}
