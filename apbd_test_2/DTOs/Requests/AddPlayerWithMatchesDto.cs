using System.ComponentModel.DataAnnotations;

namespace apbd_test_2.DTOs.Requests;

public class AddPlayerWithMatchesDto
{
    [MaxLength(50, ErrorMessage = "First name length must be <= 50 characters")]
    public string FirstName { get; set; }
    [MaxLength(100, ErrorMessage = "Last name length must be <= 100 characters")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Birth date is required")]
    public DateTime BirthDate { get; set; }
    [Required]
    public List<MatchDto> Matches { get; set; }

    public class MatchDto
    {
        public int MatchId { get; set; }
        public int MVPs { get; set; }
        [Range(0.01, 99.99, ErrorMessage = "The range for rating is 0.01-99.99")]
        public double Rating { get; set; }
    }
}