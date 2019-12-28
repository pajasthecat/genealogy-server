using System.Threading;
using System.Threading.Tasks;
using Geneology.Api.Models.Responses;
using Geneology.Api.Queries;
using Geneology.Infrastructure.Repositories;
using MediatR;

namespace Geneology.Api.Handlers.QueryHandlers
{
    public class GetFamilyMemberByIdHandler : IRequestHandler<GetFamilyMemberByIdQuery, GetFamilyMemberResponse>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public GetFamilyMemberByIdHandler(IFamilyMembersRepository familyMemberRepository)
        {
            this._familyMembersRepository = familyMemberRepository;
        }
        public async Task<GetFamilyMemberResponse> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _familyMembersRepository.GetFamilyMemberById(request.Id);
            if (result == null) return null;

            return new GetFamilyMemberResponse(
                result.Id.ToString(),
                result.Name,
                result.Birth,
                result.Death);
        }
    }
}