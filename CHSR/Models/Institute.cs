﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class Institute
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Faculty> Faculties { get; set; }
    }


}

