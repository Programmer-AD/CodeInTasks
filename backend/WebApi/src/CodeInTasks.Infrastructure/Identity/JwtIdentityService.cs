using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Domain.Enums;
using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CodeInTasks.Infrastructure.Identity
{
    internal class JwtIdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly JwtAuthOptions authOptions;

        public JwtIdentityService(
            UserManager<User> userManager,
            IMapper mapper,
            JwtAuthOptions authOptions)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.authOptions = authOptions;
        }

        public async Task CreateUserAsync(UserCreateDto userCreateDto)
        {
            var password = userCreateDto.Password;

            var userData = mapper.Map<UserData>(userCreateDto);
            var user = new User()
            {
                UserName = userCreateDto.Email,
                Email = userCreateDto.Email,
                UserData = userData,
            };

            var result = await userManager.CreateAsync(user, password);
            AssertResultSucceeded(result);
        }

        public async Task<UserSignInResultDto> SignInAsync(string email, string password)
        {
            var user = await GetUserByUserNameAsync(email);

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, password);

            if (isPasswordCorrect)
            {
                var claims = await GetClaimsAsync(user);
                var expires = GetExpireDate();
                var jwtToken = GetJwtToken(claims, expires);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(jwtToken);

                var result = new UserSignInResultDto()
                {
                    Token = tokenString,
                    ExpirationDate = expires,
                    User = await MapUserData(user),
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserData> GetUserInfoAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            var userData = await MapUserData(user);

            return userData;
        }

        public async Task SetBanAsync(Guid userId, bool isBanned)
        {
            var user = await GetUserByIdAsync(userId);

            user.UserData.IsBanned = isBanned;

            var result = await userManager.UpdateAsync(user);
            AssertResultSucceeded(result);
        }

        public async Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave)
        {
            var user = await GetUserByIdAsync(userId);
            var roleName = RoleNames.FromEnum(role);

            var settingTask = isHave ? userManager.AddToRoleAsync(user, roleName) : userManager.RemoveFromRoleAsync(user, roleName);

            var result = await settingTask;
            AssertResultSucceeded(result);
        }

        private static void AssertResultSucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errorCodes = result.Errors.Select(x => x.Code);
                throw new IdentityException(errorCodes);
            }
        }

        private async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            return user ?? throw new EntityNotFoundException($"Not found User with UserName=\"{userName}\"");
        }

        private async Task<User> GetUserByIdAsync(Guid userId)
        {
            var idString = userId.ToString();
            var user = await userManager.FindByIdAsync(idString);

            return user ?? throw new EntityNotFoundException(nameof(User), userId);
        }

        private async Task<UserData> MapUserData(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var userData = user.UserData;
            userData.Roles = roles.Select(x => Enum.Parse<RoleEnum>(x));

            return userData;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var claims = await userManager.GetClaimsAsync(user);

            return claims;
        }

        private static DateTime GetExpireDate()
        {
            var expires = DateTime.UtcNow.Add(IdentityConstants.TokenExpirationTime);

            return expires;
        }

        private JwtSecurityToken GetJwtToken(IEnumerable<Claim> claims, DateTime expires)
        {
            var signingCredentials = new SigningCredentials(authOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return jwtToken;
        }


    }
}
