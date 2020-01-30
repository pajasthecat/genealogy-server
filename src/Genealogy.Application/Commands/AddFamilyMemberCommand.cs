using System;
using System.Collections.Generic;
using Genealogy.Application.Models;
using MediatR;

namespace Genealogy.Application.Commands
{
    public class AddFamilyMemberCommand : IRequest<FamilyMember>
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