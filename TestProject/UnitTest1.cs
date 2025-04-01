using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuth.Business.AuthService.Interface;
using JWTAuth.Controllers;
using JWTAuth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace unitTesting
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithUser()
        {
            // Arrange
            var loginUser = new LoginUser { UserName = "testuser", Password = "password" };
            var user = new User("testuser", "Test User", "password", "Admin");
            _mockAuthService.Setup(service => service.Login(loginUser.UserName, loginUser.Password)).ReturnsAsync(user);

            // Act
            var result = await _authController.Login(loginUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(loginUser.UserName, returnValue.UserName);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenUserNameIsEmpty()
        {
            // Arrange
            var loginUser = new LoginUser { UserName = "", Password = "password" };

            // Act
            var result = await _authController.Login(loginUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email address needs to entered", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WithUser()
        {
            // Arrange
            var registerUser = new RegisterUser { UserName = "testuser", Name = "Test User", Password = "password", Role = "Admin" };
            var user = new User(registerUser.UserName, registerUser.Name, registerUser.Password, registerUser.Role);
            _mockAuthService.Setup(service => service.Register(It.IsAny<User>())).ReturnsAsync(user);
            _mockAuthService.Setup(service => service.Login(registerUser.UserName, registerUser.Password)).ReturnsAsync(user);

            // Act
            var result = await _authController.Register(registerUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(registerUser.UserName, returnValue.UserName);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenNameIsEmpty()
        {
            // Arrange
            var registerUser = new RegisterUser { UserName = "testuser", Name = "", Password = "password", Role = "Admin" };

            // Act
            var result = await _authController.Register(registerUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Name needs to entered", ((dynamic)badRequestResult.Value).message);
        }

        [Fact]
        public void Test_ReturnsOkResult_WithClaims()
        {
            
            var token = "Bearer testtoken";
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.CreateJwtSecurityToken();
            var claims = new Dictionary<string, string>();
            foreach (var claim in jwt.Claims)
            {
                claims.Add(claim.Type, claim.Value);
            }

            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _authController.ControllerContext.HttpContext.Request.Headers["Authorization"] = token;

            // Act
            var result = _authController.Test();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Dictionary<string, string>>(okResult.Value);
            Assert.Equal(claims, returnValue);
        }
    }
}