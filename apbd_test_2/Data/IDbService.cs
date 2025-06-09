using apbd_test_2.DTOs.Requests;
using apbd_test_2.DTOs.Responses;

namespace apbd_test_2.Data;

public interface IDbService
{
    public Task<PlayerWithMatchesDto> GetPlayerMatches(int playerId);

    public Task<int> AddPlayerWithMatches(AddPlayerWithMatchesDto addPlayerWithMatchesDto);
}