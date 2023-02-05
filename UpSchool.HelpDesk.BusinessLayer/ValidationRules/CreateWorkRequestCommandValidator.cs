using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.CreateWorkRequest;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class CreateWorkRequestCommandValidator : AbstractValidator<CreateWorkRequestCommand>
    {
        public CreateWorkRequestCommandValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty();
            this.RuleFor(x=>x.Description).NotEmpty();
        }
    }
}
