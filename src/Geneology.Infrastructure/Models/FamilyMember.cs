using System;

namespace Geneology.Infrastructure.Models
{
    public class FamilyMember
    {
        public FamilyMember()
        { }

        public FamilyMember(
             string id,
             string firstname,
             string lastname,
             DateTime birth,
             DateTime? death,
             string congregation)
        {
            Id = id;
            BirthDate = birth;
            DeathDate = death;
            Firstname = firstname;
            Lastname = lastname;
            Congregation = congregation;
        }

        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public string Congregation { get; set; }
    }
}