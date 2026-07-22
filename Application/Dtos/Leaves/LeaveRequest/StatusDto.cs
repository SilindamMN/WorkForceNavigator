namespace Application.Dtos.Leaves.LeaveRequest
{
    using Domain.Constants.Enums;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class StatusDto
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status status { get; set; }
  }
}