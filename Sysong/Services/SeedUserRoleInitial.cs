using Microsoft.AspNetCore.Identity;

namespace Sysong.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if(!_roleManager.RoleExistsAsync("Supervisor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Supervisor";
                role.NormalizedName = "SUPERVISOR";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if(_userManager.FindByEmailAsync("admin@admin").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@admin";
                user.Email = "admin@admin";
                user.NormalizedUserName = "ADMIN@ADMIN";
                user.NormalizedEmail = "ADMIN@ADMIN";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult userResult = _userManager.CreateAsync(user, "#Admin2022").Result;

                if(userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin");
                }
            } else
            {
                Console.WriteLine("Achei");
            }

            if (_userManager.FindByEmailAsync("supervisor@supervisor").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "supervisor@supervisor";
                user.Email = "supervisor@supervisor";
                user.NormalizedUserName = "SUPERVISOR@SUPERVISOR";
                user.NormalizedEmail = "SUPERVISOR@SUPERVISOR";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult userResult = _userManager.CreateAsync(user, "#Supervisor2022").Result;

                if (userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Supervisor");
                }
            }
        }
    }
}
