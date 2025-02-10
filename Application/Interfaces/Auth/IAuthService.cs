
namespace Application.Interfaces.Auth
{
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public interface IAuthService
  {
    Task<GeneralServiceResponseDto> SeedRolesAsync();
    Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginServiceResponseDto?> LoginAsync(LoginDto loginDto);
    Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User,UpdateRoleDto roleDto);
    Task<LoginServiceResponseDto?> MeAsync(MeDto meDto);
    Task<IEnumerable<UserInfoResult>> GetUserListAsync();
    Task<UserInfoResult?> GetUserDetailsByUserNamesync(string userName);
    Task<UserDetailsDto> GetUserExtraDetailsByUserNameAsync(string userName);
    Task<IEnumerable<string>> GetUsernamesListAsync();
    Task<GeneralServiceResponseDto> UpdateUserDetails(string username, UpdateUserDetailsDto userDetailsDto);
  }
}