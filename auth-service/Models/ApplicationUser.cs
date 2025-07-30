namespace auth_service.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string Cpf { get; set; } = string.Empty;
    }

}
