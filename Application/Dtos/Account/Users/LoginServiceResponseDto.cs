namespace Application.Dtos.Account.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LoginServiceResponseDto
  {
    public string NewToken { get; set; } = string.Empty;
    public UserInfoResult UserInfo { get; set; } = new UserInfoResult();
  }
}