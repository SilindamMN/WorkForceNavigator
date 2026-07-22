namespace Application.Dtos.Account.Users
{
    using Domain.Constants.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateRoleDto
  {
    [Required(ErrorMessage = "Username Required")]
    public string Username { get; set; } = string.Empty;
    public RoleType NewRole { get; set; }
  }
}