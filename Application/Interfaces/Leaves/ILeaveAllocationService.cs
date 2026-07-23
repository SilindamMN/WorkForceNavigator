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
    Task<IEnumerable<EmployeeLeaveAllocationDto>> GetMyLeavesAllocationsAsync(ClaimsPrincipal user);
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsAsync();
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsByLeaveTypeAsync(string leaveName);
    Task<GeneralServiceResponseDto> CreateLeaveAllocationAsync(string username);
  }
}