namespace Application.Interfaces.Auth
{
    using Application.Dtos.Account.Messages;
    using Domain.Dtos.General;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMessageService
  {
    Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal user, CreateMessageDto createMessageDto);
    Task<IEnumerable<GetMessageDto>> GetMessagesAsync();
    Task<IEnumerable<GetMessageDto>> GetMyMessageAsync(ClaimsPrincipal user);
  }
}