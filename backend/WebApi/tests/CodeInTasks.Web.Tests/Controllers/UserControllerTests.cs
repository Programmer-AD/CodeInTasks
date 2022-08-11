using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CodeInTasks.Application.Abstractions;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Application.Abstractions.Interfaces.Services;
using CodeInTasks.Domain.Enums;
using CodeInTasks.Domain.Models;
using CodeInTasks.Web.Controllers;
using CodeInTasks.Web.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeInTasks.Web.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private static readonly Guid userId = Guid.NewGuid();
        private static readonly UserSignInModel userSignInModel = new();
        private static readonly UserSignInResultDto userSignInResultDto = new();
        private static readonly UserSignInResultModel userSignInResultModel = new();
        private static readonly UserCreateModel userCreateModel = new();
        private static readonly UserCreateDto userCreateDto = new();
        private static readonly UserData userData = new();
        private static readonly UserViewModel userViewModel = new();
        private static readonly BanManageModel banManageModel = new();
        private static readonly RoleManageModel roleManageModel = new();

        private Mock<IUserService> userServiceMock;
        private Mock<IMapper> mapperMock;

        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            userServiceMock = new();
            mapperMock = new();

            userController = new(
                userServiceMock.Object,
                mapperMock.Object);

            SetupMappings();
        }

        [Test]
        public async Task SignInAsync_WhenSucceeded_ReturnOkObjectResultWithValue()
        {
            SetupServiceSignInResult(success: true);


            var result = await userController.SignInAsync(userSignInModel);


            result.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result.Result;
            okObjectResult.Value.Should().NotBeNull();
        }

        [Test]
        public async Task SignInAsync_WhenFailed_ReturnUnathorizedResult()
        {
            SetupServiceSignInResult(success: false);


            var result = await userController.SignInAsync(userSignInModel);


            result.Result.Should().BeOfType<UnauthorizedResult>();
        }

        [Test]
        public async Task RegisterAsync_CallServiceCreateOnce()
        {
            await userController.RegisterAsync(userCreateModel);


            userServiceMock.Verify(x => x.CreateAsync(It.IsAny<UserCreateDto>()), Times.Once);
        }

        [Test]
        public async Task GetAsync_CallServiceGetOnce()
        {
            await userController.GetAsync(userId);


            userServiceMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task SetRoleAsync_CallServiceSetRoleOnce()
        {
            await userController.SetRoleAsync(roleManageModel);


            userServiceMock.Verify(x => x.SetRoleAsync(It.IsAny<Guid>(), It.IsAny<RoleEnum>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public async Task SetBanAsync_CallServiceSetBanOnce()
        {
            await userController.SetBanAsync(banManageModel);


            userServiceMock.Verify(x => x.SetBanAsync(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
        }

        private void SetupMappings()
        {
            mapperMock
                .Setup(x => x.Map<UserSignInResultModel>(It.IsAny<UserSignInResultDto>()))
                .Returns(userSignInResultModel);

            mapperMock
                .Setup(x => x.Map<UserCreateDto>(It.IsAny<UserCreateModel>()))
                .Returns(userCreateDto);

            mapperMock
                .Setup(x => x.Map<UserViewModel>(It.IsAny<UserData>()))
                .Returns(userViewModel);
        }

        private void SetupServiceSignInResult(bool success)
        {
            var result = success ? userSignInResultDto : null;

            userServiceMock
                .Setup(x => x.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(result);
        }
    }
}
