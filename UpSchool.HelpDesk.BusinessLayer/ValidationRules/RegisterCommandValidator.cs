using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.RegisterRequest;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            this.RuleFor(x => x.EmailAddress).EmailAddress();
            this.RuleFor(x => x.EmailAddress).NotEmpty();
            this.RuleFor(x=>x.Name).NotEmpty();
            this.RuleFor(x => x.Password).NotEmpty();
        }
    }
}
