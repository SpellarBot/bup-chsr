using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.Models
{
    public class ApplicationAttachment : Entity
    {
        public string FileName { get; set; }
        public string FileCategory { get; set; }

        public AdmissionApplication AdmissionApplication { get; set; }
    }
}
