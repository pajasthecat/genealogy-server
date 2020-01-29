using System.Collections.Generic;
using Genealogy.Application.Models;
using MediatR;

namespace Genealogy.Application.Queries
{
    public class GetFamilyMembersQuery : IRequest<List<FamilyMember>>
    { }
}