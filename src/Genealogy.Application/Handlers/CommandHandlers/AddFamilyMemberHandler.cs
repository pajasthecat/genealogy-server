using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Genealogy.Application.Commands;
using Genealogy.Application.Models;
using Genealogy.Application.Repositories;
using MediatR;

namespace Genealogy.Application.Handlers.CommandHandlers
{
    public class AddFamilyMemberHandler : IRequestHandler<AddFamilyMemberCommand, FamilyMember>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public AddFamilyMemberHandler(IFamilyMembersRepository familyMembersRepository)
        {
            _familyMembersRepository = familyMembersRepository;
        }

        public async Task<FamilyMember> Handle(AddFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var result = await _familyMembersRepository.AddFamilyMemberAsync(
                new FamilyMember(
                    Guid.NewGuid(),
                    request.Firstname,
                    request.Lastname,
                    request.BirthDate,
                    request.DeathDate,
                    request.Congregation));

            if (request.Relationships != null)
                await _familyMembersRepository.AddRelationshipsAsync(
                    new FamilyMember(
                        result.Id,
                        request.Firstname,
                        request.Lastname,
                        request.BirthDate,
                        request.DeathDate,
                        request.Congregation),
                    request.Relationships.ToDictionary(
                    rel => rel.Key,
                    rel => (Relationships)Enum.Parse(typeof(Relationships), rel.Value.ToString())));

            return new FamilyMember(
                result.Id,
                result.Firstname,
                result.Lastname,
                result.BirthDate,
                result.DeathDate,
                result.Congregation);
        }
    }
}