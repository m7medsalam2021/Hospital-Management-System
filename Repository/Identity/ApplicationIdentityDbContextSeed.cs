using Hospital.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repository.Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    Name = "Mohamed Sallam",
                    Email = "Mohamed@gmail.com",
                    UserName = "m7medsallam",
                    PhoneNumber = "01021245191"
                };
                await userManager.CreateAsync(user, "P@$$W0rd");
            }
        }
    }
}
