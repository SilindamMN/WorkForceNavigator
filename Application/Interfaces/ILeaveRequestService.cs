namespace Application.Interfaces
{
  using System;
  using System.Collections.Generic;
  using System.Security.Claims;
  using System.Threading.Tasks;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Dtos.LeaveRequest;
  using Domain.Enums;

  public interface ILeaveRequestService
  {
    Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequests();
    Task<IEnumerable<LeaveRequestDto>> GetUpComingLeaves();
    Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByUser(string username);
    Task<LeaveRequestDto> GetLeaveRequestsById(int requestId);
    Task<GeneralServiceResponseDto> CreateLeaveRequest(ClaimsPrincipal User,CreateLeaveRequestDto createLeaveRequestDto);
    Task<GeneralServiceResponseDto> UpdateLeaveRequest(ClaimsPrincipal User, int leaveRequestId, UpdateLeaveRequestDto updateLeaveRequestDto);
    Task<GeneralServiceResponseDto> DeleteLeaveRequest(ClaimsPrincipal User,int leaveRequestId);
    Task<GeneralServiceResponseDto> ProcessLeaveRequest(ClaimsPrincipal User, int leaveRequestId, Status status);

  }
}