using System.Threading;
using System.Threading.Tasks;
using Genealogy.Application.Models;
using Genealogy.Application.Queries;
using Genealogy.Application.Repositories;
using MediatR;

namespace Genealogy.Application.Handlers.QueryHandlers
{
    public class GetFamilyMemberByIdHandler : IRequestHandler<GetFamilyMemberByIdQuery, FamilyMember>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public GetFamilyMemberByIdHandler(IFamilyMembersRepository familyMemberRepository)
        {
            this._familyMembersRepository = familyMemberRepository;
        }
        public Task<FamilyMember> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var result = _familyMembersRepository.GetFamilyMemberById(request.Id);
            if (result == null) return Task.FromResult<FamilyMember>(null);
            return Task.FromResult(
                new FamilyMember(
                result.Id,
                result.Firstname,
                result.Lastname,
                result.BirthDate,
                result.DeathDate,
                result.Congregation));
        }
    }
}