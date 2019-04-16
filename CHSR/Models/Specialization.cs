using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public ICollection<SubSpecialization> SubSpecializations { get; set; }
    }
}
