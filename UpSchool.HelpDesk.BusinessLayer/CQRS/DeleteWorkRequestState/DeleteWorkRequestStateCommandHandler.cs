using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.DeleteWorkRequestState
{
    public class DeleteWorkRequestStateCommandHandler : IRequestHandler<DeleteWorkRequestStateCommand, Result<NoContent>>
    {
        private readonly IUow uow;

        public DeleteWorkRequestStateCommandHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(DeleteWorkRequestStateCommand request, CancellationToken cancellationToken)
        {
            var deletedData = await this.uow.GetRepository<WorkRequestState>().FindAsync(x => x.Id == request.Id);
            if(deletedData != null)
            {
                this.uow.GetRepository<WorkRequestState>().Remove(deletedData);
                await this.uow.CommitAsync();
            }

            return new Result<NoContent>(true);
        }
    }
}
