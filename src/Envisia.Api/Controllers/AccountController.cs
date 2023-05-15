using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Library;
using Envisia.Library.Security;
using Envisia.Library.Security.Models;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    public class AccountController : EnvisiaControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtResolver _jwtResolver;
        private readonly RsaResolver _rsaResolver;
        private readonly IUserService _userService;

        public AccountController(IConfiguration configuration, JwtResolver jwtResolver, RsaResolver rsaResolver, IUserService userService)
        {
            _configuration = configuration;
            _jwtResolver = jwtResolver;
            _rsaResolver = rsaResolver;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var plainPassword = _rsaResolver.Decrypt(request.Password);

            ServiceResult<UserDto> userResult = await _userService.FindByUserNameAsync(request.UserName);
            if (!userResult.Success)
            {
                throw new UnauthorizedAccessException("User does not exist.");
            }

            UserDto user = userResult.Result;

            ServiceResult<bool> validUserResult = await _userService.CheckPasswordAsync(user.UserName, plainPassword);
            if (!validUserResult.Success)
            {
                throw new UnauthorizedAccessException("User name or password is invalid.");
            }

            var userClaims = new UserClaims
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
                Roles = request.Roles ?? Enumerable.Empty<string>()
            };

            string accessToken = _jwtResolver.GenerateAccessToken(userClaims);

            string refreshToken = await StoreAndReturnRefreshToken();

            var tokenResponse = new JwtTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(tokenResponse);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> GenerateRefreshToken(AuthRefreshTokenRequest request)
        {
            if (request == null)
            {
                throw new UnauthorizedAccessException("Invalid payload for refresh token.");
            }

            ServiceResult<RefreshTokenDto> refreshTokenResult = await _userService.GetRefreshTokenAsync();
            if (!refreshTokenResult.Success)
            {
                throw new UnauthorizedAccessException("Refresh token does not exist.");
            }

            if (refreshTokenResult.Result?.Token != request.RefreshToken)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
            else if (refreshTokenResult.Result?.Expires >= DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token expired.");
            }

            ServiceResult<UserDto> userResult = await _userService.FindByUserNameAsync(CurrentUserName);
            if (!userResult.Success)
            {
                throw new UnauthorizedAccessException("Cannot retrieve user.");
            }

            UserDto user = userResult.Result;

            var userClaims = new UserClaims
            {
                Id = user.Id.ToString(),
                Name = user.UserName,
                Roles = request.Roles ?? Enumerable.Empty<string>()
            };

            string accessToken = _jwtResolver.GenerateAccessToken(userClaims);

            string refreshToken = await StoreAndReturnRefreshToken();

            var tokenResponse = new JwtTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return Ok(tokenResponse);
        }

        private async Task<string> StoreAndReturnRefreshToken()
        {
            var refreshToken = _jwtResolver.GenerateRefreshToken();

            var refreshTokenDto = new RefreshTokenDto
            {
                Token = refreshToken.Token,
                Created = refreshToken.Created,
                Expires = refreshToken.Expires
            };

            await _userService.SaveTenantRefreshTokenAsync(refreshTokenDto);

            return refreshToken.Token;
        }
    }
}
