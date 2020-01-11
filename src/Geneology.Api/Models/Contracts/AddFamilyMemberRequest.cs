using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geneology.Api.Models.Contracts
{
    public class AddFamilyMemberRequest
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string Congregation { get; set; }

        public Dictionary<string, string> Relationships { get; set; }
    }
}