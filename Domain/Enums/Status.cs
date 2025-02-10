namespace Domain.Enums
{
  using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Text.Json.Serialization;
  using System.Threading.Tasks;


  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum Status
  {
    Approved,
    Declined,
    Pending
  }
  }
