using System;
using System.Collections.Generic;
using Geneology.Api.Models.Contracts;
using Geneology.Api.Models.Responses;
using MediatR;

namespace Geneology.Api.Commands
{
    public class AddFamilyMemberCommand : IRequest<GetFamilyMemberResponse>
    {
        public AddFamilyMemberCommand(
            string firstname,
            string lastname,
            DateTime birthDate,
            DateTime? deathDate,
            string congregation,
            Dictionary<Guid, Relationships> relationships)
        {
            Firstname = firstname;
            Lastname = lastname;
            BirthDate = birthDate;
            DeathDate = deathDate;
            Congregation = congregation;
            Relationships = relationships;
        }

        public string Firstname { get; }

        public string Lastname { get; }

        public DateTime BirthDate { get; }

        public DateTime? DeathDate { get; }

        public string Congregation { get; }

        public Dictionary<Guid, Relationships> Relationships { get; }
    }
}