using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.UpdateWorkRequest;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class UpdateWorkRequestCommandValidator : AbstractValidator<UpdateWorkRequestCommand>
    {
        public UpdateWorkRequestCommandValidator()
        {
            this.RuleFor(x=>x.Title).NotEmpty();
            this.RuleFor(x=>x.Description).NotEmpty();
        }
    }
}
