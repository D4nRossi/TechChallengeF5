namespace auth_service.Data
{
    using auth_service.Models;
    using Microsoft.AspNetCore.Identity;

    public class DbInitializer
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Gerente", "Atendente", "Cozinha", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await userManager.FindByEmailAsync("gerente@fasttech.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "gerente@fasttech.com",
                    Email = "gerente@fasttech.com",
                    Cpf = "12345678900",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, "Senha@123");
                await userManager.AddToRoleAsync(user, "Gerente");
            }
        }
    }

}
