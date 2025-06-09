using System.ComponentModel.DataAnnotations;

namespace apbd_test_2.Models;

public class Map
{
    [Key]
    public int MapId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
}