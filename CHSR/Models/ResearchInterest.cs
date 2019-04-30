using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class ResearchInterest
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public ResourcePerson ResourcePerson { get; set; }
    }
}
