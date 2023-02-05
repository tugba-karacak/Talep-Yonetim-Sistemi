using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.DeleteWorkRequestState
{
    public class DeleteWorkRequestStateCommand : IRequest<Result<NoContent>>
    {
        public int Id { get; set; }

        public DeleteWorkRequestStateCommand(int id)
        {
            Id = id;
        }
    }
}
