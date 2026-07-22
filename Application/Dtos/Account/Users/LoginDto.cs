using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account.Users
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Username Required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }
  }
}