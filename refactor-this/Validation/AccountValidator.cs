using FluentValidation;
using refactor_this.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_this.Validation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("The account name cannot be blank.");

        }
    }
}