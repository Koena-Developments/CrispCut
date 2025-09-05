using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.DTOs.AtristServiceDTO
{
     public class CurrentUserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsArtist { get; set; }
    }
}