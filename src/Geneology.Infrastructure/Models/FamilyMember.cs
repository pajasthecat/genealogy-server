using System;

namespace Geneology.Infrastructure.Models
{
    public class FamilyMember
    {
        public FamilyMember()
        { }

        public FamilyMember(string name, DateTime birth, DateTime? death)
        {
            Id = Guid.NewGuid();
            Name = name;
            Birth = birth;
            Death = death;
        }

        public FamilyMember(Guid id, string name, DateTime birth, DateTime? death)
        {
            Id = id;
            Name = name;
            Birth = birth;
            Death = death;

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public DateTime? Death { get; set; }
    }
}