using System;
using FluentValidation;
using Geneology.Api.Commands;

namespace Geneology.Api.Validation
{
    public class AddFamilyMemberValidator : AbstractValidator<AddFamilyMemberCommand>
    {
        public AddFamilyMemberValidator()
        {
            RuleFor(x => x.BirthDate < DateTime.Now);
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull();
        }
    }
}