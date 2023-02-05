using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<NoContent>>
    {
        private readonly IUow uow;

        public UpdateProfileCommandHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<NoContent>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var updatedProfile = await this.uow.GetRepository<ApplicationUser>().FindAsync(x => x.Id == request.Id);
            if (updatedProfile != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Image))
                    updatedProfile.Image = request.Image;
                if (!string.IsNullOrWhiteSpace(request.Name))
                    updatedProfile.Name = request.Name;

                await this.uow.CommitAsync();
            }

            return new Result<NoContent>();
        }
    }
}
