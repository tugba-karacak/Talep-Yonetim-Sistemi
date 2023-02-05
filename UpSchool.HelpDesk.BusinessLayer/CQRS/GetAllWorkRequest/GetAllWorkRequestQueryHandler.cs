using AutoMapper;
using MediatR;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetAllWorkRequest
{
    public class GetAllWorkRequestQueryHandler : IRequestHandler<GetAllWorkRequestQuery, Result<List<WorkRequestDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public GetAllWorkRequestQueryHandler(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<List<WorkRequestDto>>> Handle(GetAllWorkRequestQuery request, CancellationToken cancellationToken)
        {
            var mappedData = _mapper.Map<List<WorkRequestDto>>( await _uow.GetRepository<WorkRequest>().GetAllAsync());
            return new Result<List<WorkRequestDto>>(mappedData);
        }
    }
}
