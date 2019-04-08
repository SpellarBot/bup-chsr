
using Microsoft.AspNetCore.Identity;

namespace CHSR.Domain.UAM
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
