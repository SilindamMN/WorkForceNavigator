namespace Application.Dtos.Account.Messages
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class GetMessageDto
  {
    public long Id { get; set; }
    public string SenderUserName { get; set; } = string.Empty; 
    public string ReceiverUserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }= DateTime.Now;
  }
}