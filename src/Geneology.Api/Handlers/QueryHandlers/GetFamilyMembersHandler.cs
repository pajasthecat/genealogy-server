using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Geneology.Api.Models.Responses;
using Geneology.Api.Queries;
using Geneology.Infrastructure.Repositories;
using MediatR;

namespace Geneology.Api.Handlers.QueryHandlers
{
    public class GetFamilyMembersHandler : IRequestHandler<GetFamilyMembersQuery, List<GetFamilyMemberResponse>>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public GetFamilyMembersHandler(IFamilyMembersRepository familyMembersRepository)
        {
            _familyMembersRepository = familyMembersRepository;
        }
        
        public async Task<List<GetFamilyMemberResponse>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            var result = _familyMembersRepository.GetFamilyMembers();

            return result.Select(r => new GetFamilyMemberResponse(
               r.Id.ToString(),
               r.Name,
               r.Birth,
               r.Death)).ToList();
        }
    }
}