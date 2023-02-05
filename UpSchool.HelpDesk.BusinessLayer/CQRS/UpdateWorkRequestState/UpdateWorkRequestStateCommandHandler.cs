using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequestState
{
    public class UpdateWorkRequestStateCommandHandler : IRequestHandler<UpdateWorkRequestStateCommand, Result<NoContent>>
    {
        private readonly IUow uow;

        public UpdateWorkRequestStateCommandHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(UpdateWorkRequestStateCommand request, CancellationToken cancellationToken)
        {
            var updatedData  = await this.uow.GetRepository<WorkRequestState>().FindAsync(x=>x.Id== request.Id);
            if (updatedData != null)
            {
                updatedData.Description = request.Description;
            }

            await this.uow.CommitAsync();

            return new Result<NoContent>(true);
        }
    }
}
