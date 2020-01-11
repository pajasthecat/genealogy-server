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

            return familyMember;
        }

        public async Task AddRelationshipsAsync(FamilyMember familyMember, Dictionary<Guid, Relationships> relationships)
        {
            foreach (var relationship in relationships)
            {
                var relativeId = relationship.Key.ToString();
                var relativeName = relationship.Value.ToString();

                await _client
                .Cypher
                .Match("(relative:FamilyMember), (related:FamilyMember)")
                .Where((FamilyMember relative) => relative.Id == relativeId)
                .AndWhere((FamilyMember related) => related.Id == familyMember.Id)
                .Create($"(relative)-[:{relativeName}]->(related)")
                .ExecuteWithoutResultsAsync();
            }
        }

        public FamilyMember GetFamilyMemberById(Guid id)
        {
            return _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Where((FamilyMember familyMember) => familyMember.Id == id.ToString())
           .Return(familyMember => familyMember.As<FamilyMember>())
           .Results.FirstOrDefault();
        }

        public IEnumerable<FamilyMember> GetFamilyMembers()
        {
            return _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Return(familyMember => familyMember.As<FamilyMember>())
           .Results;
        }
    }
}