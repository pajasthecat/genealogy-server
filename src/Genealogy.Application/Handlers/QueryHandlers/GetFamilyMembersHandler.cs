using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Genealogy.Application.Models;
using Genealogy.Application.Queries;
using Genealogy.Application.Repositories;
using MediatR;

namespace Genealogy.Application.Handlers.QueryHandlers
{
    public class GetFamilyMembersHandler : IRequestHandler<GetFamilyMembersQuery, List<FamilyMember>>
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        public GetFamilyMembersHandler(IFamilyMembersRepository familyMembersRepository)
        {
            _familyMembersRepository = familyMembersRepository;
        }

        public Task<List<FamilyMember>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            var results = _familyMembersRepository.GetFamilyMembers();

            return Task.FromResult(
                results.Select(res =>
                new FamilyMember(
                    res.Id,
                    res.Firstname,
                    res.Lastname,
                    res.BirthDate,
                    res.DeathDate,
                    res.Congregation)).ToList());
        }
    }
}