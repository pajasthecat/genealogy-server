using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Genealogy.Application.Models;
using Genealogy.Application.Repositories;

namespace Geneology.Infrastructure.Repositories.Cache
{
    public class CachedFamilyMembersRepository : IFamilyMembersRepository
    {
        private readonly IFamilyMembersRepository _familyMembersRepository;

        private readonly ConcurrentDictionary<Guid, FamilyMember> _cachedFamilyMembers = new ConcurrentDictionary<Guid, FamilyMember>();

        public CachedFamilyMembersRepository(IFamilyMembersRepository familyMembersRepository)
        {
            _familyMembersRepository = familyMembersRepository;
        }

        public async Task<Genealogy.Application.Models.FamilyMember> AddFamilyMemberAsync(Genealogy.Application.Models.FamilyMember familyMember)
        {
            return await _familyMembersRepository.AddFamilyMemberAsync(familyMember);
        }

        public async Task AddRelationshipsAsync(FamilyMember familyMember, Dictionary<Guid, Relationships> relationships)
        {
            await _familyMembersRepository.AddRelationshipsAsync(familyMember, relationships);
        }

        public FamilyMember GetFamilyMemberById(Guid id)
        {
            if (_cachedFamilyMembers.ContainsKey(id)) return _cachedFamilyMembers[id];
            var familyMember = _cachedFamilyMembers.GetOrAdd(id, _familyMembersRepository.GetFamilyMemberById(id));
            return familyMember;
        }

        public IEnumerable<FamilyMember> GetFamilyMembers()
        {
            return _familyMembersRepository.GetFamilyMembers();
        }
    }
}