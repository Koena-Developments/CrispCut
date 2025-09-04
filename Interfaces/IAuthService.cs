using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut;
using CrispCut.DTOs.UserDto;

namespace CrispCut.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(UserForRegistrationDto userForRegistration);
        Task<UserDto> LoginAsynce(UserForLoginDto userForLogin);
    }
}