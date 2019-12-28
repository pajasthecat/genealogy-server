using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geneology.Infrastructure.Models;

namespace Geneology.Infrastructure.Repositories
{
    public interface IFamilyMembersRepository
    {
        FamilyMember GetFamilyMemberById(Guid id);
        IEnumerable<FamilyMember> GetFamilyMembers();
        Task<FamilyMember> AddFamilyMemberAsync(FamilyMember familyMember);
    }
}