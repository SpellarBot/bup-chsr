using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class Department
    {
        public int id { get; set; }
        public string Name { get; set; }

        public Faculty Faculty { get; set; }

    }
}
