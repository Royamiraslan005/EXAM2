using Microsoft.AspNetCore.Identity;

namespace Greenhost.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
