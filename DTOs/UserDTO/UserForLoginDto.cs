using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CrispCut.DTOs.UserDto
{
    public class UserForLoginDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
}