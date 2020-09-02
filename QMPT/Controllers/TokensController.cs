using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMPT.Models.Requests.Users;
using QMPT.Data.Services;
using QMPT.Exceptions.Users;
using Microsoft.Extensions.Options;
using QMPT.Helpers;
using QMPT.Models.Responses;
using QMPT.Exceptions.RefreshTokens;
using System;
using QMPT.Exceptions;
using QMPT.Data.Models;
using QMPT.Entities;

namespace QMPT.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly RefreshTokensService refreshTokensService;
        private readonly UsersService usersService;
        private readonly AppSettings appSettings;

        public TokensController(
            IOptions<AppSettings> appSettingsOptions, 
            RefreshTokensService refreshTokensService, 
            UsersService usersService)
        {
            appSettings = appSettingsOptions.Value;
            this.refreshTokensService = refreshTokensService;
            this.usersService = usersService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetNewTokens([FromQuery] AuthorizationData authorizationData)
        {
            var user = usersService.Get(authorizationData.Login);
            if (user.IsRemoved)
            {
                throw new UserWasRemovedException();
            }
            if (user.Password != authorizationData.Password)
            {
                throw new UserWrongPasswordException();
            }

            var jwt = new Jwt(appSettings, user);
            var refreshToken = new RefreshToken(user);
            refreshTokensService.Insert(refreshToken);

            var tokens = new Tokens(jwt, refreshToken);

            return Ok(tokens);
        }

        [HttpGet("{token}")]
        [AllowAnonymous]
        public IActionResult GetNetTokens(string token)
        {
            var refreshToken = refreshTokensService.Get(token);
            if (refreshToken.ExpirationTime <= DateTime.UtcNow)
            {
                throw new ExpiredRefreshTokenException();
            }
            /*if (refreshToken.UserId != User.GetId())
            {
                throw new AccessDeniedException();
            }*/

            refreshToken.User = usersService.Get(refreshToken.UserId);
            if (refreshToken.User.IsRemoved)
            {
                throw new UserWasRemovedException();
            }
            refreshToken.RefreshData();
            refreshTokensService.Update(refreshToken);
            var jwt = new Jwt(appSettings, refreshToken.User);

            var tokens = new Tokens(jwt, refreshToken);

            return Ok(tokens);
        }

        [HttpDelete("{token}")]
        public IActionResult DeleteRefreshToken(string token)
        {
            var refreshToken = refreshTokensService.Get(token);
            if (refreshToken.UserId != User.GetId())
            {
                throw new AccessDeniedException();
            }
            refreshTokensService.Delete(refreshToken);

            return NoContent();
        }
    }
}
