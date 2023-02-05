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

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState
{
    public class CreateWorkRequestStateCommandHandler : IRequestHandler<CreateWorkRequestStateCommand, Result<WorkRequestStateDto>>
    {
        private readonly IUow uow;
        private readonly IMapper mapper;

        public CreateWorkRequestStateCommandHandler(IUow uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Result<WorkRequestStateDto>> Handle(CreateWorkRequestStateCommand request, CancellationToken cancellationToken)
        {
            var result = await this.uow.GetRepository<WorkRequestState>().CreateAsync(new WorkRequestState
            {
                Description = request.Description,
                WorkRequestId = request.WorkRequestId,
            });

            await this.uow.CommitAsync();

            var mappedData = this.mapper.Map<WorkRequestStateDto>(result);

            return new Result<WorkRequestStateDto>(mappedData);
        }
    }
}
