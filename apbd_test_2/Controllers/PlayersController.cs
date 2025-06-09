using apbd_test_2.Data;
using apbd_test_2.DTOs.Requests;
using apbd_test_2.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace apbd_test_2.Controllers;

[Controller]
[Route("api/players")]
public class PlayersController : ControllerBase
{
    private IDbService _dbService;

    public PlayersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}/matches")]
    public async Task<IActionResult> GetPlayerMatches(int id)
    {
        try
        {
            var playerMatches = await _dbService.GetPlayerMatches(id);
            return Ok(playerMatches);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new
            {
                message = "Player not found"
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPlayerWithMatches([FromBody] AddPlayerWithMatchesDto addPlayerWithMatchesDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var playerId = await _dbService.AddPlayerWithMatches(addPlayerWithMatchesDto);
                return Created();
            }
            catch (NotFoundException)
            {
                return NotFound(new
                {
                    message = "Match not found"
                });
            }
        }
        else
        {
            var errors = ModelState.Select(kv => kv.Value.Errors)
                .Where(mec => mec.Count > 0)
                .ToList();
            return BadRequest(errors);
        };
    }
}