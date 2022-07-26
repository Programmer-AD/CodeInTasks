using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Application.Abstractions.Interfaces.Infrastructure;
using CodeInTasks.Domain.Enums;
using CodeInTasks.Domain.Models;
using CodeInTasks.Infrastructure.Identity;
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

        private Mock<IIdentityService> identityServiceMock;
        private Mock<IMapper> mapperMock;

        private UserController userController;

        [SetUp]
        public void SetUp()
        {
            identityServiceMock = new();
            mapperMock = new();

            userController = new(
                identityServiceMock.Object,
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
        public async Task RegisterAsync_CallServiceCreateUserOnce()
        {
            await userController.RegisterAsync(userCreateModel);


            identityServiceMock.Verify(x => x.CreateUserAsync(It.IsAny<UserCreateDto>()), Times.Once);
        }

        [Test]
        public async Task GetUserInfoAsync_CallServiceGetUserInfoOnce()
        {
            SetupServiceGetUserInfoResult();


            await userController.GetUserInfoAsync(userId);


            identityServiceMock.Verify(x => x.GetUserInfoAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task SetRoleAsync_WhenUserIsAdmin_CallServiceSetRoleForAnyRole([Values] RoleEnum roleToSet)
        {
            SetUserRole(RoleNames.Admin);
            var roleManageModel = MakeRoleManageModel(roleToSet);


            await userController.SetRoleAsync(roleManageModel);


            identityServiceMock.Verify(x => x.SetRoleAsync(It.IsAny<Guid>(), roleToSet, It.IsAny<bool>()), Times.Once);
        }

        [TestCaseSource(nameof(ManagerSettableRoles))]
        public async Task SetRoleAsync_WhenUserIsManagerAndCanSetRole_CallServiceSetRole(RoleEnum roleToSet)
        {
            SetUserRole(RoleNames.Manager);
            var roleManageModel = MakeRoleManageModel(roleToSet);


            await userController.SetRoleAsync(roleManageModel);


            identityServiceMock.Verify(x => x.SetRoleAsync(It.IsAny<Guid>(), roleToSet, It.IsAny<bool>()), Times.Once);
        }

        [TestCaseSource(nameof(ManagerUnsettableRoles))]
        public async Task SetRoleAsync_WhenUserIsManagerButCantSetRole_NotCallServiceSetRole(RoleEnum roleToSet)
        {
            SetUserRole(RoleNames.Manager);
            var roleManageModel = MakeRoleManageModel(roleToSet);


            await userController.SetRoleAsync(roleManageModel);


            identityServiceMock.Verify(x => x.SetRoleAsync(It.IsAny<Guid>(), It.IsAny<RoleEnum>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public async Task SetBanAsync_CallServiceSetBanOnce()
        {
            await userController.SetBanAsync(banManageModel);


            identityServiceMock.Verify(x => x.SetBanAsync(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
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

            identityServiceMock
                .Setup(x => x.SignInAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(result);
        }

        private void SetupServiceGetUserInfoResult()
        {
            identityServiceMock
                .Setup(x => x.GetUserInfoAsync(It.IsAny<Guid>()))
                .ReturnsAsync(userData);
        }

        private void SetUserRole(string role)
        {
            var claims = new Claim[] { new(ClaimTypes.Role, role) };

            var claimsIdentity = new ClaimsIdentity(claims);

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new(claimsIdentity)
                }
            };

            userController.ControllerContext = controllerContext;
        }

        private static RoleManageModel MakeRoleManageModel(RoleEnum role)
        {
            var result = new RoleManageModel()
            {
                UserId = userId,
                Role = role,
                IsSetted = true
            };

            return result;
        }

        private static readonly IEnumerable<RoleEnum> ManagerSettableRoles
            = new[] { RoleEnum.Creator };

        private static readonly IEnumerable<RoleEnum> ManagerUnsettableRoles
            = Enum.GetValues<RoleEnum>().Except(ManagerSettableRoles).ToArray();
    }
}
