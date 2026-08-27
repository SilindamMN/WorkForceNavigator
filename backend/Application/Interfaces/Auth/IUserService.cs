using Application.Dtos.Account.Users;
using Domain.Account;
using Domain.Dtos.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IUserService
    {
        Task<IEnumerable<UserInfoResult>> GetUserListAsync();
        Task<UserInfoResult> GetUserDetailsByUserNameAsync(string userName);
        Task<UserDetailsDto> GetUserExtraDetailsByUserNameAsync(string userName);
        Task<IEnumerable<string>> GetUsernamesListAsync();
        Task<UserInfoResult> GenerateUserInfoAsync(ApplicationUser user, IList<string> roles);
        Task<GeneralServiceResponseDto> UpdateUserDetailsAsync(string username, int departmentId, UpdateUserDetailsDto userDetailsDto);
    }
}