namespace Application.Interfaces.Leaves
{
    using Application.Dtos.Leaves.LeaveAllocation;
    using Domain.Dtos.General;
    using Domain.Enties.Leaves;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILeaveAllocationService
  {
    Task<IEnumerable<EmployeeLeaveAllocationDto>> GetLeaveAllocationsByUsernameAsync(string username);
    Task<IEnumerable<EmployeeLeaveAllocationDto>> GetMyLeavesAllocationsAsync(ClaimsPrincipal User);
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsAsync();
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsByLeaveTypeAsync(string LeaveName);
    Task<GeneralServiceResponseDto> CreateLeaveAllocationAsync(string username);
  }
}