using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class ResourcePerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
        public  string PicFolderId { get; set; }

        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string SubSpecialization { get; set; }
        public string PhotoFileName { get; internal set; }

        
        public List<string> ResearchInterest { get; set; }

    }
}
