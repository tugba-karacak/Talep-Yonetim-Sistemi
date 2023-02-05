using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequestState;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class CreateWorkRequestStateCommandValidator : AbstractValidator<CreateWorkRequestStateCommand>
    {
        public CreateWorkRequestStateCommandValidator()
        {
            this.RuleFor(x=>x.WorkRequestId).NotEmpty();
            this.RuleFor(x=>x.Description).NotEmpty();
        }
    }
}
