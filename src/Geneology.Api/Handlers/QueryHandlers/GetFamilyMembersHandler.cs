using System;
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

        public Task<List<GetFamilyMemberResponse>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            var results = _familyMembersRepository.GetFamilyMembers();

            return Task.FromResult(
                results.Select(res =>
                new GetFamilyMemberResponse(
                    Guid.Parse(res.Id),
                    res.Firstname,
                    res.Lastname,
                    res.BirthDate,
                    res.DeathDate,
                    res.Congregation)).ToList());
        }
    }
}