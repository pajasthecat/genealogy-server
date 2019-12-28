using System;
using Geneology.Api.Models.Responses;
using MediatR;

namespace Geneology.Api.Queries
{
    public class GetFamilyMemberByIdQuery : IRequest<GetFamilyMemberResponse>
    {
        public Guid Id { get; }
        public GetFamilyMemberByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}