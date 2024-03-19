using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using ClickBooking.Controllers;
using ClickBooking.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickBooking.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            // Mock UserManager and SignInManager
            _mockUserManager = new Mock<UserManager<User>>();
            _mockSignInManager = new Mock<SignInManager<User>>();

            // Initialize UserController
            _userController = new UserController(_mockUserManager.Object, _mockSignInManager.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsRegisteredSuccessfully()
        {
            // Arrange
            var registerModel = new Register { Username = "TestUser", Password = "TestPassword" };

            // Act
            var result = await _userController.Register(registerModel);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        // Add more tests for other methods and scenarios
    }
}