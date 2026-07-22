namespace Application.Interfaces.Leaves
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Application.Dtos.Leaves.LeaveRequest;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;

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