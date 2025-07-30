namespace auth_service.Controllers
{
    using auth_service.Models;
    using auth_service.Models.DTOs;
    using auth_service.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            // Permitir login por email ou CPF
            var user = await _userManager.FindByEmailAsync(model.EmailOuCpf)
                ?? _userManager.Users.FirstOrDefault(u => u.Cpf == model.EmailOuCpf);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user, roles);
                return Ok(new { token, email = user.Email, roles });
            }
            return Unauthorized();
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            //e-mail já cadastrado
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return BadRequest(new { message = "Já existe um usuário com este e-mail." });

            //CPF já cadastrado
            var cpfExists = _userManager.Users.FirstOrDefault(u => u.Cpf == model.Cpf);
            if (cpfExists != null)
                return BadRequest(new { message = "Já existe um usuário com este CPF." });

            //Criar usuário
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Cpf = model.Cpf,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            //Adiciona à role Cliente
            await _userManager.AddToRoleAsync(user, "Cliente");

            return Ok(new { message = "Cliente registrado com sucesso" });
        }


        [HttpGet("segredo")]
        [Authorize(Roles = "Cliente,Gerente")]
        public IActionResult Segredo()
        {
            return Ok("Você acessou uma rota protegida!");
        }

        [HttpPost("register-gerente")]
        public async Task<IActionResult> RegisterGerente([FromBody] RegisterDto model)
        {
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return BadRequest(new { message = "Já existe um usuário com este e-mail." });

            var cpfExists = _userManager.Users.FirstOrDefault(u => u.Cpf == model.Cpf);
            if (cpfExists != null)
                return BadRequest(new { message = "Já existe um usuário com este CPF." });

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Cpf = model.Cpf,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Gerente");

            return Ok(new { message = "Gerente registrado com sucesso" });
        }


    }
}
