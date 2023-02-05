using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetUserName
{
    public class GetUserNameByIdQueryHandler : IRequestHandler<GetUserNameByIdQuery, Result<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetUserNameByIdQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

     

        public async Task<Result<string>> Handle(GetUserNameByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.Users.SingleOrDefaultAsync(x => x.Id == request.Id);
            return new Result<string>
            {
                Data = user.Name,
                IsSuccess = true,
            };
        }
    }
}
