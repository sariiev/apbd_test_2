using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_test_2.Models;

public class PlayerMatch
{
    [ForeignKey("Match")]
    public int MatchId { get; set; }
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public int MVPs { get; set; }
    [Column(TypeName = "decimal(4,2)")]
    public decimal? Rating { get; set; }
    
    public Match Match { get; set; }
    public Player Player { get; set; }
}