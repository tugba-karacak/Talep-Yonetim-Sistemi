using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetMemberUsers
{
    public class GetMemberUserListQueryHandler : IRequestHandler<GetMemberUserListQuery, Result<List<ApplicationUserDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper mapper;

        public GetMemberUserListQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this.mapper = mapper;
        }


        public async Task<Result<List<ApplicationUserDto>>> Handle(GetMemberUserListQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.GetUsersInRoleAsync("Member");
            var mappedData = this.mapper.Map<List<ApplicationUserDto>>(users);
           return new Result<List<ApplicationUserDto>>(mappedData);
        }
    }
}
