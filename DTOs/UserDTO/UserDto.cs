using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.DTOs.UserDto
{
  public class UserDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
}