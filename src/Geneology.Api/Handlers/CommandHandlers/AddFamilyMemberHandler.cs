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
                    request.Name,
                    request.BirthDate,
                    request.DeathDate));

            return new GetFamilyMemberResponse(
                 result.Id.ToString(),
                 result.Name,
                 result.Birth,
                 result.Death);
        }
    }
}