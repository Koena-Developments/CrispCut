using System.ComponentModel.DataAnnotations;

namespace CrispCut;

public class UserForRegistrationDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    public string Role { get; set; }
}