using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.DTOs.UserDTO
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserDto? User { get; set; }
        public string? Token { get; set; } 
    }
}