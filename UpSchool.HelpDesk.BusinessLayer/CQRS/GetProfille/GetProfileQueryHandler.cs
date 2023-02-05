using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetProfille
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<ApplicationUserDto>>
    {
        private readonly IUow uow;
        private readonly IMapper mapper;

        public GetProfileQueryHandler(IUow uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public async Task<Result<ApplicationUserDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user= await this.uow.GetRepository<ApplicationUser>().GetByFilterAsync(x => x.Id == request.Id); 
           var mappedData = this.mapper.Map<ApplicationUserDto>(user);

            return new Result<ApplicationUserDto>(mappedData);
        }
    }
}
