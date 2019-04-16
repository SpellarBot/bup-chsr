using System;

namespace CHSR.Models
{
    public class OrganizationExperience
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Designation { get; set; }
        public string FieldOfResearch { get; set; }
        public string Responsibility { get; set; }
    }
}
