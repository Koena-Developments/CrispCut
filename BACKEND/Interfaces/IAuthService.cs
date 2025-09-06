using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut;
using CrispCut.DTOs.AtristServiceDTO;
using CrispCut.DTOs.UserDTO;

namespace CrispCut.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUserAsync(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> LoginAsync(UserForLoginDto userForLogin);
        Task<CurrentUserDto?> GetCurrentUserAsync(int userId);
    }
}