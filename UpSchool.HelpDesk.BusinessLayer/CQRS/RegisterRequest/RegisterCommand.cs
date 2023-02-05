using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;
using UpSchool.HelpDesk.DtoLayer;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.RegisterRequest
{
    public class RegisterCommand : IRequest<Result<NoContent>>
    {
        public string EmailAddress { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
