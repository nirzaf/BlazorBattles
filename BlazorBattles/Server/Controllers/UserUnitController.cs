using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBattles.Server.Data;
using BlazorBattles.Server.Services;
using BlazorBattles.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _context.Units.FirstOrDefaultAsync<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();
            if (user.Bananas < unit.BananaCost)
            {
                return BadRequest("Not enough bananas!");
            }

            user.Bananas -= unit.BananaCost;

            UserUnit newUserUnit = new UserUnit
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.HitPoints
            };

            await _context.UserUnits.AddAsync(newUserUnit);
            await _context.SaveChangesAsync();
            return Ok(newUserUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserUnits()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();

            var response = userUnits.Select(
                unit => new UserUnitResponse
                {
                    UnitId = unit.UnitId,
                    HitPoints = unit.HitPoints
                });

            return Ok(response);
        }

        [HttpPost("revive")]
        public async Task<IActionResult> ReviveArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(unit => unit.UserId == user.Id)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int bananaCost = 1000;

            if (user.Bananas < bananaCost)
            {
                return BadRequest($"Not enough bananas! You need {bananaCost} bananas to revive your army.");
            }

            bool armyAlreadyAlive = true;
            foreach (var userUnit in userUnits)
            {
                if (userUnit.HitPoints <= 0)
                {
                    armyAlreadyAlive = false;
                    userUnit.HitPoints = new Random().Next(1, userUnit.Unit.HitPoints);
                }
            }

            if (armyAlreadyAlive)
                return Ok("Your army is already alive.");

            user.Bananas -= bananaCost;

            await _context.SaveChangesAsync();

            return Ok("Army revived!");
        }
    }
}
