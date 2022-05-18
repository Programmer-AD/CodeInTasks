using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using CodeInTasks.Application.Abstractions.Dtos.User;
using CodeInTasks.Application.Abstractions.Exceptions;
using CodeInTasks.Infrastructure.Persistance.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CodeInTasks.Infrastructure.Identity
{
    internal class JwtIdentityService : IJwtIdentityService
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

        public async Task<string> GetSignInTokenAsync(string email, string password)
        {
            var user = await GetUserByUserNameAsync(email);

            var isPasswordCorrect = await userManager.CheckPasswordAsync(user, password);

            if (isPasswordCorrect)
            {
                var claims = await GetClaimsAsync(user);
                var jwtToken = GetJwtToken(claims);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenString = tokenHandler.WriteToken(jwtToken);

                return tokenString;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserViewDto> GetUserInfoAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);

            var userViewDto = mapper.Map<UserViewDto>(user.UserData);
            userViewDto.Roles = roles.Select(x => Enum.Parse<RoleEnum>(x));

            return userViewDto;
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

            return user ?? throw new EntityNotFoundException($"Not found user with name \"{userName}\"");
        }

        private async Task<User> GetUserByIdAsync(Guid userId)
        {
            var idString = userId.ToString();
            var user = await userManager.FindByIdAsync(idString);

            return user ?? throw new EntityNotFoundException($"Not found user with id \"{userId}\"");
        }

        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(IdentityConstants.UserIdClaimType, user.Id.ToString()),
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GetJwtToken(List<Claim> claims)
        {
            var expires = DateTime.UtcNow.Add(IdentityConstants.TokenExpirationTime);
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
