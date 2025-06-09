using apbd_test_2.DTOs.Requests;
using apbd_test_2.DTOs.Responses;
using apbd_test_2.Exceptions;
using apbd_test_2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_test_2.Data;

public class DbService : IDbService
{
    private DatabaseContext _databaseContext;

    public DbService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<PlayerWithMatchesDto> GetPlayerMatches(int playerId)
    {
        var player = await _databaseContext.Players.Where(p => p.PlayerId == playerId).FirstOrDefaultAsync();
        if (player == null)
        {
            throw new NotFoundException();
        }

        var playerMatches = await _databaseContext
            .PlayerMatches
            .Include(pm => pm.Match)
            .ThenInclude(m => m.Map)
            .Include(pm => pm.Match)
            .ThenInclude(m => m.Tournament)
            .Where(pm => pm.PlayerId == playerId)
            .ToListAsync();

        return new PlayerWithMatchesDto()
        {
            PlayerId = player.PlayerId,
            FirstName = player.FirstName,
            LastName = player.LastName,
            BirthDate = player.BirthDate,
            Matches = playerMatches.Select(pm => new PlayerWithMatchesDto.MatchDto()
            {
                Date = pm.Match.MatchDate,
                Map = pm.Match.Map.Name,
                MVPs = pm.MVPs,
                Rating = Math.Round((float) pm.Rating, 2),
                Team1Score = pm.Match.Team1Score,
                Team2Score = pm.Match.Team2Score,
                Tournament = pm.Match.Tournament.Name
            }).ToList()
        };
    }

    public async Task<int> AddPlayerWithMatches(AddPlayerWithMatchesDto addPlayerWithMatchesDto)
    {
        var player = new Player()
        {
            BirthDate = addPlayerWithMatchesDto.BirthDate, FirstName = addPlayerWithMatchesDto.FirstName,
            LastName = addPlayerWithMatchesDto.LastName
        };
        var transaction = _databaseContext.Database.BeginTransaction();
        await _databaseContext.Players.AddAsync(player);

        foreach (var matchDto in addPlayerWithMatchesDto.Matches)
        {
            var match = await _databaseContext.Matches.Where(m => m.MatchId == matchDto.MatchId).FirstOrDefaultAsync();
            if (match == null)
            {
                await transaction.RollbackAsync();
                throw new NotFoundException();
            }
            else
            {
                var rating = (decimal)Math.Round(matchDto.Rating, 2);
                var playerMatch = new PlayerMatch()
                {
                    Match = match, Player = player, MVPs = matchDto.MVPs,
                    Rating = rating
                };
                if (match.BestRating < rating)
                {
                    match.BestRating = rating;
                    Console.WriteLine("Updated match rating");
                }

                await _databaseContext.PlayerMatches.AddAsync(playerMatch);
                _databaseContext.Matches.Attach(match);
            }
        }

        await _databaseContext.SaveChangesAsync();
        await _databaseContext.Database.CommitTransactionAsync();

        return player.PlayerId;
    }
}