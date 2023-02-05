using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.Results;

namespace UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<Result<NoContent>>
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
    }
}
