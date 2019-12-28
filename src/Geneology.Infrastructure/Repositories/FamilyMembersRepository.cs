using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geneology.Infrastructure.Models;
using Neo4jClient;

namespace Geneology.Infrastructure.Repositories
{
    public class FamilyMembersRepository : IFamilyMembersRepository
    {
        private readonly IGraphClient _client;

        public FamilyMembersRepository(IGraphClient client)
        {
            _client = client;
        }

        public async Task<FamilyMember> AddFamilyMemberAsync(FamilyMember familyMember)
        {
            await _client
            .Cypher
            .Create("(familyMember:FamilyMember {familyMember})")
            .WithParam("familyMember", familyMember)
            .ExecuteWithoutResultsAsync();

            return new FamilyMember(
                familyMember.Id,
                familyMember.Name,
                familyMember.Birth,
                familyMember.Death);
        }

        public FamilyMember GetFamilyMemberById(Guid id)
        {
            return _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Where((FamilyMember familyMember) => familyMember.Id == id)
           .Return(familyMember => familyMember.As<FamilyMember>())
           .Results.FirstOrDefault();
        }

        public IEnumerable<FamilyMember> GetFamilyMembers()
        {
            var familyMembers = _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Return(familyMember => familyMember.As<FamilyMember>())
           .Results;

            return familyMembers;
        }
    }
}