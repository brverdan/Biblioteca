using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Authenticate;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AuthenticateService _authenticateService;

        public AuthenticateController(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [Route("Token")]
        [HttpPost]
        //[RequireHttps]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(BadRequest(ModelState));
            }

            var token = _authenticateService.AuthenticateUser(loginRequest.Email, loginRequest.Password);

            if (String.IsNullOrEmpty(token))
            {
                return await Task.FromResult(BadRequest("Login ou senha inválidos"));
            }

            return Ok(new
            {
                Token = token
            });

        }
    }
}
