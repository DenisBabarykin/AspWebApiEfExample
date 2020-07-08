using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiExample.DB;
using WebApiExample.Dto;

namespace WebApiExample.Controllers
{
    public class FootballController : ControllerBase
    {
        private readonly FootballContext _dbContext;

        public FootballController(FootballContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetPlayers(int id)
        {
            Player player = _dbContext.Players.Include(p => p.Team).FirstOrDefault(p => p.Id == id);
            if (player != null)
                return Ok(new PlayerDto(player.Id, player.Name, player.Team.TeamName));
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult PostPlayers([FromBody] PlayerDto playerDto)
        {
            var team = _dbContext.Teams
                .FirstOrDefault(t => t.TeamName == playerDto.TeamName);

            if (team == null)
            {
                int id = _dbContext.Teams.Any() ? _dbContext.Teams.Max(t => t.Id) + 1 : 0;
                team = new Team()
                {
                    Id = id,
                    TeamName = playerDto.TeamName
                };
                _dbContext.Teams.Add(team);
                _dbContext.SaveChanges();
            }

            var player = new Player()
            {
                Id = playerDto.Id,
                Name = playerDto.Name,
                TeamId = team.Id,
                Team = team
            };
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}