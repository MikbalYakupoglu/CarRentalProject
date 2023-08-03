using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto);

            if (!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }

            var result = _authService.CreateAccessToken(registerResult.Data);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.Success)
            {
                return Unauthorized(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return Ok(result);
        }

        [HttpGet("get-claims/{email}")]
        public IActionResult GetUserClaims(string email)
        {
            var userClaims = _authService.GetUserClaims(email);
            return Ok(userClaims);
        }
    }
}
