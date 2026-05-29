namespace Domain.Entities
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Message : BaseEntity<long>
  {
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public string Text { get; set; }
  }
}
