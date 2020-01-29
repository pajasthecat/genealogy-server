using System;
using Genealogy.Application.Models;
using MediatR;

namespace Genealogy.Application.Queries
{
    public class GetFamilyMemberByIdQuery : IRequest<FamilyMember>
    {
        public Guid Id { get; }
        public GetFamilyMemberByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}