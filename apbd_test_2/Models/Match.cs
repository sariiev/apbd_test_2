using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_test_2.Models;

public class Match
{
    [Key]
    public int MatchId { get; set; }
    [ForeignKey("Tournament")]
    public int TournamentId { get; set; }
    [ForeignKey("Map")]
    public int MapId { get; set; }
    [Required]
    public DateTime MatchDate { get; set; }
    public int Team1Score { get; set; }
    public int Team2Score { get; set; }
    [Column(TypeName = "decimal(4,2)")]
    public decimal? BestRating { get; set; }

    public Map Map { get; set; }
    public Tournament Tournament { get; set; }
}