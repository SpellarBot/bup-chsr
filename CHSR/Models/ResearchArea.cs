using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class ResearchArea
    {
        public int Id { get; set; }
        public int ResourcePersonId { get; set; }
        public ResourcePerson ResourcePerson { get; set; }
        public int ResearchInterestId { get; set; }
        public ResearchInterest ResearchInterest { get; set; }
        
    }
}
