using System;
using FluentValidation;
using Genealogy.Application.Commands;

namespace Genealogy.Application.Validation
{
    public class AddFamilyMemberValidator : AbstractValidator<AddFamilyMemberCommand>
    {
        public AddFamilyMemberValidator()
        {
            RuleFor(x => x.BirthDate < DateTime.Now);
            RuleFor(x => x.Firstname)
            .NotEmpty()
            .NotNull();
            RuleFor(x => x.Lastname)
            .NotEmpty()
            .NotNull();
            RuleFor(x => x.Congregation)
            .NotEmpty()
            .NotNull();
        }
    }
}