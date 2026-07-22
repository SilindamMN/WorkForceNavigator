namespace Application.Dtos.Account.Messages
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class CreateMessageDto
  {
    public string ReceiverUserName { get; set; } = string.Empty;    
    public string Text { get; set; } = string.Empty;
  }
}