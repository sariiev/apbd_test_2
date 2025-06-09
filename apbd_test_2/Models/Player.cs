using System.ComponentModel.DataAnnotations;

namespace apbd_test_2.Models;

public class Player
{
    [Key]
    public int PlayerId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
}