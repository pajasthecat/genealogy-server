using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Genealogy.Application.Models;

namespace Genealogy.Application.Repositories
{
    public interface IFamilyMembersRepository
    {
        FamilyMember GetFamilyMemberById(Guid id);
        IEnumerable<FamilyMember> GetFamilyMembers();
        Task<FamilyMember> AddFamilyMemberAsync(FamilyMember familyMember);
        Task AddRelationshipsAsync(FamilyMember familymember, Dictionary<Guid, Relationships> relationships);
    }
}