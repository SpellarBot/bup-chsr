using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHSR.ViewModels
{
    public class AttachmentViewModel
    { 
        public List<IFormFile> ApplicationAttachmentFiles { get; set; }
    }
}
