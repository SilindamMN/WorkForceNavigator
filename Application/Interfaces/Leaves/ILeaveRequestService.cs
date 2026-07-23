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
        Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync();
        Task<IEnumerable<LeaveRequestDto>> GetUpComingLeavesAsync();
        Task<IEnumerable<LeaveRequestDto?>> GetLeaveRequestsByUserAsync(string username);
        Task<LeaveRequestDto?> GetLeaveRequestsByIdAsync(int requestId);
        Task<GeneralServiceResponseDto> CreateLeaveRequestAsync(ClaimsPrincipal User, CreateLeaveRequestDto createLeaveRequestDto);
        Task<GeneralServiceResponseDto> UpdateLeaveRequestAsync(ClaimsPrincipal User, int leaveRequestId, UpdateLeaveRequestDto updateLeaveRequestDto);
        Task<GeneralServiceResponseDto> DeleteLeaveRequestAsync(ClaimsPrincipal User, int leaveRequestId);
        Task<GeneralServiceResponseDto> ProcessLeaveRequestAsync(ClaimsPrincipal User, int leaveRequestId, Status status);

    }
}