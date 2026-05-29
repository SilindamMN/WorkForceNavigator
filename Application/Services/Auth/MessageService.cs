namespace Application.Services.Auth
{
  using Application.Helpers;
  using Application.Interfaces.Auth;
  using AutoMapper;
  using Domain.Account;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.EntityFrameworkCore;
  using Persistence;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public class MessageService : IMessageService
  {
    private readonly IMapper mapper;
    private readonly DataContext dataContext;
    private readonly ILogService logService;
    private readonly UserManager<ApplicationUser> userManager;

    public MessageService(IMapper mapper ,DataContext dataContext,ILogService logService,UserManager<ApplicationUser> userManager )
    {
      this.mapper = mapper;
      this.dataContext = dataContext;
      this.logService = logService;
      this.userManager = userManager;
    }
    public async Task<GeneralServiceResponseDto> CreateNewMessageAsync(ClaimsPrincipal user, CreateMessageDto createMessageDto)
    {
      if (user.Identity.Name == createMessageDto.ReceiverEmail)
      {
        return ResponseHelper.CreateResponse(false, 400, "Sender and Receiver cannot be the same");
      }

      var validReceiverName = await userManager.Users.AnyAsync(u => u.UserName == createMessageDto.ReceiverEmail);
      if (!validReceiverName)
      {
        return ResponseHelper.CreateResponse(false, 400, "Receiver Username not valid");
      }

      var message = new Message()
      {
        SenderEmail = user.Identity.Name,
          ReceiverEmail = createMessageDto.ReceiverEmail,
        Text = createMessageDto.Text
      };

      await dataContext.Messages.AddAsync(message);
      await dataContext.SaveChangesAsync();
      await logService.SaveNewLog(user.Identity.Name, "Send Message");

      return ResponseHelper.CreateResponse(true, 200, "Message Saved Successfully");
    }

    public async Task<IEnumerable<GetMessageDto>> GetMessagesAsync()
    {
      var messages = await dataContext.Messages.OrderByDescending(m=>m.CreatedAt).ToListAsync();
      return mapper.Map<IEnumerable<GetMessageDto>>(messages);
    }

    public async Task<IEnumerable<GetMessageDto>> GetMyMessageAsync(ClaimsPrincipal User)
    {
      var loggedInUser = User.Identity.Name;
      var messages = await dataContext.Messages
          .Where(m => m.SenderEmail == loggedInUser || m.ReceiverEmail == loggedInUser)
          .OrderByDescending(m => m.CreatedAt)
          .ToListAsync();

      return mapper.Map<IEnumerable<GetMessageDto>>(messages);
    }
  }
}