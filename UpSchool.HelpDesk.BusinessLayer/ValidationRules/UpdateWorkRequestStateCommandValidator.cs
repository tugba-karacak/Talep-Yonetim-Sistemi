using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequestState;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class UpdateWorkRequestStateCommandValidator : AbstractValidator<UpdateWorkRequestStateCommand>
    {
        public UpdateWorkRequestStateCommandValidator()
        {
            this.RuleFor(x=>x.Description).NotEmpty();
        }
    }
}
