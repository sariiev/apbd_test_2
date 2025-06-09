using Microsoft.EntityFrameworkCore;

namespace apbd_test_2.Models;

public class DatabaseContext : DbContext
{
    public DbSet<Map> Maps { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<PlayerMatch> PlayerMatches { get; set; }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerMatch>()
            .HasKey(pm => new { pm.MatchId, pm.PlayerId });

        modelBuilder.Entity<Map>()
            .HasData(
                new Map() { MapId = 1, Type = "Type 1", Name = "Map 1" }, 
                new Map() { MapId = 2, Type = "Type 2", Name = "Map 2" },
                new Map() { MapId = 3, Type = "Type 2", Name = "Map 3" });

        modelBuilder.Entity<Player>()
            .HasData(
                new Player() { PlayerId = 1, BirthDate = DateTime.Today, FirstName = "Bob", LastName = "Smith" },
                new Player()
                    { PlayerId = 2, BirthDate = DateTime.Today.AddDays(1), FirstName = "Alex", LastName = "Green" });

        modelBuilder.Entity<Tournament>()
            .HasData(
                new Tournament()
                {
                    TournamentId = 1, Name = "Tournament 1", StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(5)
                },
                new Tournament()
                {
                    TournamentId = 2, Name = "Tournament 2", StartDate = DateTime.Today.AddDays(5),
                    EndDate = DateTime.Today.AddDays(15)
                });

        modelBuilder.Entity<Match>()
            .HasData(
                new Match()
                {
                    MatchId = 1, MapId = 1, Team1Score = 1, Team2Score = 2, TournamentId = 1, BestRating = 0.5m, MatchDate = DateTime.Today.AddDays(1)
                },
                new Match()
                {
                    MatchId = 2, MapId = 2, Team1Score = 5, Team2Score = 6, TournamentId = 1, BestRating = 1.0m, MatchDate = DateTime.Today
                });

        modelBuilder.Entity<PlayerMatch>()
            .HasData(
                new PlayerMatch()
                {
                    MatchId = 1, MVPs = 3, PlayerId = 1, Rating = 1.5m
                },
                new PlayerMatch()
                {
                    MatchId = 2, MVPs = 3, PlayerId = 2, Rating = 2.5m
                },
                new PlayerMatch()
                {
                    MatchId = 2, MVPs = 2, PlayerId = 1, Rating = 5.5m
                });
    }
}