using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.GetUserName
{
    public class GetUserNameByIdQuery : IRequest<Result<string>>
    {
        public int Id { get; set; }

        public GetUserNameByIdQuery(int id)
        {
            Id = id;
        }
    }
}
