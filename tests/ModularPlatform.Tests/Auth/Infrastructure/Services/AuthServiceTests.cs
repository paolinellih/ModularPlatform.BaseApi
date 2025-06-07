using Modules.Auth.Application.Requests;
using Modules.Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Modules.Logging.Application.Interfaces;

namespace ModularPlatform.Tests.Modules.Auth.Infrastructure.Services;

public class AuthServiceTests
{
    private readonly Mock<ILoggingService<AuthService>> _loggerMock;
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _loggerMock = new Mock<ILoggingService<AuthService>>();
        var userStoreMock = new Mock<IUserStore<IdentityUser>>();
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        _configMock = new Mock<IConfiguration>();
        _configMock.Setup(c => c["Jwt:Key"]).Returns("super_secret_test_key_that_is_long_enough_123456");
        _configMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
        _configMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

        _authService = new AuthService(_loggerMock.Object, _userManagerMock.Object, _configMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new IdentityUser { Email = "test@example.com", Id = "123" };
        var request = new LoginRequest { Email = "test@example.com", Password = "password123" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(user, request.Password))
            .ReturnsAsync(true);

        // Act
        var token = await _authService.LoginAsync(request);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "newuser@example.com",
            Password = "Test123!"
        };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), request.Password))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.RegisterAsync(request);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldReturnSuccess_WhenPasswordIsChanged()
    {
        // Arrange
        var user = new IdentityUser { Id = "user1", Email = "user1@test.com" };

        _userManagerMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);
        _userManagerMock.Setup(x =>
            x.ChangePasswordAsync(user, "oldPass", "newPass"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.ChangePasswordAsync(user.Id, "oldPass", "newPass");

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task GeneratePasswordResetTokenAsync_ShouldReturnToken()
    {
        // Arrange
        var user = new IdentityUser { Email = "user@example.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(user))
            .ReturnsAsync("reset-token");

        // Act
        var token = await _authService.GeneratePasswordResetTokenAsync(user.Email);

        // Assert
        Assert.Equal("reset-token", token);
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldReturnSuccess()
    {
        // Arrange
        var user = new IdentityUser { Email = "user@example.com" };
        _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
        _userManagerMock.Setup(x =>
            x.ResetPasswordAsync(user, "reset-token", "NewPass123!"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.ResetPasswordAsync(user.Email, "reset-token", "NewPass123!");

        // Assert
        Assert.True(result.Succeeded);
    }
}