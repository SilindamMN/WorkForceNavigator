using Domain.Entities;

namespace Domain.Enties.Hr
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class Message : BaseEntity<long>
  {
    public string SenderUsername { get; set; } = string.Empty;
        public string ReceiverUserName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
  }
}
