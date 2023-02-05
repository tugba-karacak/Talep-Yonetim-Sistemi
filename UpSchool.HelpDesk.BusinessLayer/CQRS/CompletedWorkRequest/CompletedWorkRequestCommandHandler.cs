using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Defaults;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.CompletedWorkRequest
{
    public class CompletedWorkRequestCommandHandler : IRequestHandler<CompletedWorkRequestCommand, Result<NoContent>>
    {
        private readonly IUow uow;

        public CompletedWorkRequestCommandHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(CompletedWorkRequestCommand request, CancellationToken cancellationToken)
        {
             var updatedData = await this.uow.GetRepository<WorkRequest>().FindAsync(x => x.Id == request.Id);

            if (updatedData != null)
            {
                updatedData.State = WorkRequestStateDefaults.Completed;
            }
            await this.uow.CommitAsync();

            return new Result<NoContent>(true);
        }
    }
}
