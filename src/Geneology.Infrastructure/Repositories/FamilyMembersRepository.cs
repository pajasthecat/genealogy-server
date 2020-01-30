using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genealogy.Application.Models;
using Genealogy.Application.Repositories;
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
            var dtoFamilyMember = new Dto.FamilyMember(
                familyMember.Id.ToString(),
                familyMember.Firstname,
                familyMember.Lastname,
                familyMember.BirthDate,
                familyMember.DeathDate,
                familyMember.Congregation);

            foreach (var relationship in relationships)
            {
                var relativeId = relationship.Key.ToString();
                var relativeName = relationship.Value.ToString();

                await _client
                .Cypher
                .Match("(relative:FamilyMember), (related:FamilyMember)")
                .Where((Dto.FamilyMember relative) => relative.Id == relativeId)
                .AndWhere((Dto.FamilyMember related) => related.Id == dtoFamilyMember.Id)
                .Create($"(relative)-[:{relativeName}]->(related)")
                .ExecuteWithoutResultsAsync();
            }
        }

        public FamilyMember GetFamilyMemberById(Guid id)
        {
            var result = _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Where((Dto.FamilyMember familyMember) => familyMember.Id == id.ToString())
           .Return(familyMember => familyMember.As<Dto.FamilyMember>())
           .Results.FirstOrDefault();

            return result == null
            ? null
            : new FamilyMember(
                 new Guid(result.Id),
                 result.Firstname,
                 result.Lastname,
                 result.BirthDate,
                 result.DeathDate,
                 result.Congregation);
        }

        public IEnumerable<FamilyMember> GetFamilyMembers()
        {
            var result = _client
           .Cypher
           .Match("(familyMember:FamilyMember)")
           .Return(familyMember => familyMember.As<Dto.FamilyMember>())
           .Results;

            return result
            .Select(r => new FamilyMember(
                new Guid(r.Id),
                r.Firstname,
                r.Lastname,
                r.BirthDate,
                r.DeathDate,
                r.Congregation));
        }
    }
}