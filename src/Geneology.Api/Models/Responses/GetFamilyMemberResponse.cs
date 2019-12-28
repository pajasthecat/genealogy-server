using System;

namespace Geneology.Api.Models.Responses
{
    public class GetFamilyMemberResponse
    {
        public GetFamilyMemberResponse(string id, string name, DateTime birthDate, DateTime? deathDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            DeathDate = deathDate;
        }

        public string Id { get; }
        public string Name { get; }
        public DateTime BirthDate { get; }
        public DateTime? DeathDate { get; }
    }
}