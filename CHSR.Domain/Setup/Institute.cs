
using System.ComponentModel.DataAnnotations;

namespace CHSR.Domain.Setup
{
    public class Institute : Entity
    {
        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }
    }
}
