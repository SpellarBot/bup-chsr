using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class Faculty
    {
        public int ID { get; set; }
        public string Name { get; set; }

        //public int InstituteID { get; set; }

        //[ForeignKey("InstituteID")]
        public Institute Institute { get; set; }

    }
}
