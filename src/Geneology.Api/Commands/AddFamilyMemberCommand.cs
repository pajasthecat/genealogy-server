using System;
using Geneology.Api.Models.Responses;
using MediatR;

namespace Geneology.Api.Commands
{
    public class AddFamilyMemberCommand : IRequest<GetFamilyMemberResponse>
    {
        public AddFamilyMemberCommand(string name, DateTime birthDate, DateTime? deathDate)
        {
            Name = name;
            BirthDate = birthDate;
            DeathDate = deathDate;
        }
        
        public string Name { get; }

        public DateTime BirthDate { get; }

        public DateTime? DeathDate { get; }        
    }
}