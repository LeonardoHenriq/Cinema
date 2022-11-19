using Cinema.Application.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUserNameAsync(string username);

        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);

        Task<UserDto> CreateAccountAsync(UserDto userDto);

        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}
