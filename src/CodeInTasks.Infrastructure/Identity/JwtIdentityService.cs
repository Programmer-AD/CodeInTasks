using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using CodeInTasks.Application.Dtos.User;
using CodeInTasks.Application.Exceptions;
using CodeInTasks.Infrastructure.Exceptions;
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
            JwtAuthOptions jwtAuthOptions)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.authOptions = jwtAuthOptions;
        }

        public async Task CreateUserAsync(UserCreateDto userCreateDto)
        {
            var password = userCreateDto.Password;
            var user = mapper.Map<User>(userCreateDto);

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }
        }

        public async Task<string> GetSignInTokenAsync(string userName, string password)
        {
            var user = await GetUserByNameAsync(userName);

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

            var userViewDto = mapper.Map<UserViewDto>(user);
            userViewDto.Roles = roles.Select(x => Enum.Parse<RoleEnum>(x));

            return userViewDto;
        }

        public async Task SetBanAsync(Guid userId, bool isBanned)
        {
            var user = await GetUserByIdAsync(userId);

            user.IsBanned = isBanned;

            await userManager.UpdateAsync(user);
        }

        public async Task SetRoleAsync(Guid userId, RoleEnum role, bool isHave)
        {
            var user = await GetUserByIdAsync(userId);
            var roleName = RoleNames.FromEnum(role);

            var settingTask = isHave ? userManager.AddToRoleAsync(user, roleName) : userManager.RemoveFromRoleAsync(user, roleName);
            var result = await settingTask;

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors);
            }
        }

        private async Task<User> GetUserByNameAsync(string userName)
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
