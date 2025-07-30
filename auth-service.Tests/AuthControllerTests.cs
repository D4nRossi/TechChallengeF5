using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using auth_service.Controllers;
using auth_service.Models;
using auth_service.Models.DTOs;
using auth_service.Services;

public class AuthControllerTests
{
    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenUserDoesNotExist()
    {
        var userManager = Mock.Of<UserManager<ApplicationUser>>();
        var signInManager = Mock.Of<SignInManager<ApplicationUser>>();
        var jwtService = new Mock<JwtService>(null);

        var controller = new AuthController(userManager, signInManager, jwtService.Object);

        var result = await controller.Login(new LoginDto
        {
            Email = "naoexiste@email.com",
            Password = "senha"
        });

        Assert.IsType<UnauthorizedResult>(result);
    }
}