using Microsoft.AspNetCore.Identity;

namespace sdq.Entities
{
    public class AppUser :IdentityUser
    {
        public string FullName { get; set; }
    }
}
