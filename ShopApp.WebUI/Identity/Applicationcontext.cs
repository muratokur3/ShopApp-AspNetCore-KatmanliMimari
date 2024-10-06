using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.WebUI.Identity
{
    public class Applicationcontext:IdentityDbContext<User>
    {
        public Applicationcontext(DbContextOptions<Applicationcontext> options):base(options)
        {
            
        }
    }
}
