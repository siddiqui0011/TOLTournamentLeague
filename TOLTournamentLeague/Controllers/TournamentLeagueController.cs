using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TOLTournamentLeague.DOM;
using TOLTournamentLeague.LeagueRepository;

namespace TOLTournamentLeague.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class TournamentLeaguesController : ControllerBase
    //{
    //    private readonly ILeagueRepository _tournamentLeagueService;

    //    public TournamentLeaguesController(ILeagueRepository tournamentLeagueService)
    //    {
    //        _tournamentLeagueService = tournamentLeagueService;
    //    }

    //    // GET: api/tournamentleagues/1
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<TournamentLeague>> GetTournamentLeague(int id)
    //    {
    //        try
    //        {
    //            var league = await _tournamentLeagueService.GetLeagueByIdAsync(id);

    //            // Check if the league is active
    //            if (league.Is_Active == 0)
    //            {
    //                return BadRequest("League is not active right now.");
    //            }

    //            // Check if another league is active based on the current league ID
    //            if (id == 1)
    //            {
    //                // Cricket League is active, so Football League should be inactive
    //                var footballLeague = await _tournamentLeagueService.GetLeagueByIdAsync(2);
    //                if (footballLeague != null && footballLeague.Is_Active == 1)
    //                {
    //                    return BadRequest("Football League is active, Cricket League cannot be active.");
    //                }
    //            }
    //            else if (id == 2)
    //            {
    //                // Football League is active, so Cricket League should be inactive
    //                var cricketLeague = await _tournamentLeagueService.GetLeagueByIdAsync(1);
    //                if (cricketLeague != null && cricketLeague.Is_Active == 1)
    //                {
    //                    return BadRequest("Cricket League is active, Football League cannot be active.");
    //                }
    //            }

    //            return Ok(new TournamentLeague { Id = league.Id, Title = league.Title });

    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, $"Internal server error: {ex.Message}");
    //        }
    //    }

    //this is original code
    

    [Route("api/[controller]")]
    [ApiController]
    public class TournamentLeaguesController : ControllerBase
    {
        private readonly ILeagueRepository _tournamentLeagueService;

        public TournamentLeaguesController(ILeagueRepository tournamentLeagueService)
        {
            _tournamentLeagueService = tournamentLeagueService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentLeague>>> GetAllLeagues()
        {
            try
            {
                var leagues = await _tournamentLeagueService.GetAllLeaguesAsync();
                return Ok(leagues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateLeague")]
        public async Task<ActionResult> AddLeague([FromBody] string name)
        {
            try
            {
                await _tournamentLeagueService.AddLeagueAsync(name);
                return CreatedAtAction(nameof(GetAllLeagues), new { name }, name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        // GET: api/tournamentleagues/1
        // [HttpGet("{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentLeague>> GetTournamentLeague(int id)
        {
            try
            {
                var league = await _tournamentLeagueService.GetLeagueByIdAsync(id);

                if (league == null)
                {
                    return NotFound();
                }
                if (!league.Is_Active)
                {
                    return BadRequest("League is not active right now.");
                }
                return Ok(new TournamentLeague { Id = league.Id, Title = league.Title, Is_Active = league.Is_Active });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("active")]
        public async Task<ActionResult<TournamentLeague>> GetActiveLeague()
        {
            try
            {
                var league = await _tournamentLeagueService.GetActiveLeagueAsync();

                if (league == null)
                {
                    return NotFound("No active league found.");
                }
                return Ok(new TournamentLeague { Id = league.Id, Title = league.Title, Is_Active = league.Is_Active });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("activate/{id}")]
        public async Task<IActionResult> ActivateLeague(int id)
        {
            try
            {
                await _tournamentLeagueService.ActivateLeagueAsync(id);
                return Ok("League activated successfully.(Only One League Activate At the Same Time)");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
