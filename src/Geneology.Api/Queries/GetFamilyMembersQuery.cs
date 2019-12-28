using System.Collections.Generic;
using Geneology.Api.Models.Responses;
using MediatR;

namespace Geneology.Api.Queries
{
    public class GetFamilyMembersQuery : IRequest<List<GetFamilyMemberResponse>>
    {
        
    }
}