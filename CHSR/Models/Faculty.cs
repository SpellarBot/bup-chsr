using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class Faculty
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Institute Institute { get; set; }
    }
}
