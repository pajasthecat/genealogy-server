using System;
using System.ComponentModel.DataAnnotations;

namespace Geneology.Api.Models.Contracts
{
    public class AddFamilyMemberRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }
    }
}