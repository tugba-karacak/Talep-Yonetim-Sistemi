using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.CompletedWorkRequest
{
    public class CompletedWorkRequestCommand : IRequest<Result<NoContent>>
    {
        public int Id { get; set; }

        public CompletedWorkRequestCommand(int id)
        {
            Id = id;
        }
    }
}
