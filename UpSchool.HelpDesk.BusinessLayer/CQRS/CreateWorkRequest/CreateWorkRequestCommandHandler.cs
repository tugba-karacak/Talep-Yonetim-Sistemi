using AutoMapper;
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

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequest
{
    public class CreateWorkRequestCommandHandler : IRequestHandler<CreateWorkRequestCommand, Result<WorkRequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public CreateWorkRequestCommandHandler(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<WorkRequestDto>> Handle(CreateWorkRequestCommand request, CancellationToken cancellationToken)
        {
            var createdData = _mapper.Map<WorkRequest>(request);
            createdData.State = WorkRequestStateDefaults.Waiting;
            createdData.CreatedDate = DateTime.Now;
            var mappedData = _mapper.Map<WorkRequestDto>( await _uow.GetRepository<WorkRequest>().CreateAsync(createdData));


            await _uow.CommitAsync();

            return new Result<WorkRequestDto>(mappedData);
        }
    }
}
