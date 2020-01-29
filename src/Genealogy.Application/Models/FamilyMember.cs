using System;

namespace Genealogy.Application.Models
{
    public class FamilyMember
    {
        public FamilyMember(Guid id, string firstname, string lastname, DateTime birthDate, DateTime? deathDate, string congregation)
        {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.BirthDate = birthDate;
            this.DeathDate = deathDate;
            this.Congregation = congregation;
        }

        public Guid Id { get; }
        public string Firstname { get; }
        public string Lastname { get; }
        public DateTime BirthDate { get; }
        public DateTime? DeathDate { get; }
        public string Congregation { get; }
    }
}