using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequest
{
    public class UpdateWorkRequestCommandHandler : IRequestHandler<UpdateWorkRequestCommand, Result<NoContent>>
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public UpdateWorkRequestCommandHandler(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<NoContent>> Handle(UpdateWorkRequestCommand request, CancellationToken cancellationToken)
        {
             var updatedData= await _uow.GetRepository<WorkRequest>().FindAsync(x => x.Id == request.Id);

            if (updatedData != null)
            {           
                updatedData.Title = request.Title;
                updatedData.Description = request.Description;
                await _uow.CommitAsync();
            }

            return new Result<NoContent>(true);
        }
    }
}
