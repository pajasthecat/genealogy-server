using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Geneology.Api.Commands;
using Geneology.Api.Models.Responses;
using Geneology.Infrastructure.Models;
using Geneology.Infrastructure.Repositories;
using MediatR;

namespace Geneology.Api.Handlers.CommandHandlers
{
    public class AddFamilyMemberHandler : IRequestHandler<AddFamilyMemberCommand, GetFamilyMemberResponse>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public AddFamilyMemberHandler(IFamilyMembersRepository familyMembersRepository)
        {
            _familyMembersRepository = familyMembersRepository;
        }

        public async Task<GetFamilyMemberResponse> Handle(AddFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var result = await _familyMembersRepository.AddFamilyMemberAsync(
                new FamilyMember(
                    Guid.NewGuid().ToString(),
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

            return new GetFamilyMemberResponse(
                Guid.Parse(result.Id),
                result.Firstname,
                result.Lastname,
                result.BirthDate,
                result.DeathDate,
                result.Congregation);
        }
    }
}