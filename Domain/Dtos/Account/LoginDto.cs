using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account
{
  public class LoginDto
  {
    [Required(ErrorMessage = "Email Required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password Required")]
    public string Password { get; set; }
  }
}