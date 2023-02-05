using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.AssignUser;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class AssignUserCommandValidator : AbstractValidator<AssignUserCommand>
    {
        public AssignUserCommandValidator()
        {
            this.RuleFor(x=>x.UserId).NotEmpty();
            this.RuleFor(x=>x.WorkRequestId).NotEmpty();
        }
    }
}
