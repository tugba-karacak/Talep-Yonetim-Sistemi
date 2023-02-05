using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.BusinessLayer.CQRS.LoginRequest;

namespace UpSchool.HelpDesk.BusinessLayer.ValidationRules
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            this.RuleFor(x => x.Email).NotEmpty();
            this.RuleFor(x => x.Email).EmailAddress();
            this.RuleFor(x => x.Password).NotEmpty();
        }
    }
}
