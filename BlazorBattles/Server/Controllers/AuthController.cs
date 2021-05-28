using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBattles.Server.Data;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            var response = await _authRepo.Register(
                    new User
                    {
                        Username = request.Username,
                        Email = request.Email,
                        Bananas = request.Bananas,
                        DateOfBirth = request.DateOfBirth,
                        IsConfirmed = request.IsConfirmed
                    },
                    request.Password,
                    int.Parse(request.StartUnitId)
                );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            var response = await _authRepo.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
