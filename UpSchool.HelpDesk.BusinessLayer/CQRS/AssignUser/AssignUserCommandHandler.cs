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

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.AssignUser
{
    public class AssignUserCommandHandler : IRequestHandler<AssignUserCommand, Result<NoContent>>
    {
        private readonly IUow uow;

        public AssignUserCommandHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            var updatedWorkRequest = await this.uow.GetRepository<WorkRequest>().FindAsync(x => x.Id == request.WorkRequestId);

            if(updatedWorkRequest != null)
            {
                updatedWorkRequest.State = WorkRequestStateDefaults.Working;
                updatedWorkRequest.AssignedUserId = request.UserId;

                await this.uow.CommitAsync();
            }
         

            return new Result<NoContent>()
            {
                IsSuccess = true,
                Message = "işlem başarılı"
            };
        }
    }
}
