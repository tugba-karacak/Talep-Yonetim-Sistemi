using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DtoLayer.Statics;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetStatics
{
    public class GetStaticForGraphQueryHandler : IRequestHandler<GetStaticForGraphQuery, Result<List<StaticsForGraphDto>>>
    {
        private readonly IUow uow;

        public GetStaticForGraphQueryHandler(IUow uow)
        {
            this.uow = uow;
        }

        public async Task<Result<List<StaticsForGraphDto>>> Handle(GetStaticForGraphQuery request, CancellationToken cancellationToken)
        {
            var personelQuery = this.uow.GetRepository<ApplicationUser>().GetQueryable();
            var workReqeustQuery = this.uow.GetRepository<WorkRequest>().GetQueryable();

            var grouppedQuery = from p in personelQuery
                              join w in workReqeustQuery
                              on p.Id equals w.AssignedUserId
                              group w by new { p.Email } into g
                              select new StaticsForGraphDto
                              {
                                  Email = g.Key.Email,
                                  Count = g.Count()
                              };

            var data = grouppedQuery.ToList();


            return new Result<List<StaticsForGraphDto>>(data);
        }
    }
}
