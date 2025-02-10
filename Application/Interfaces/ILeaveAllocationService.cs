namespace Application.Interfaces
{
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Dtos.LeaveAllocation;
  using Domain.Enties.Leaves;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public interface ILeaveAllocationService
  {
    Task<IEnumerable<EmployeeLeaveAllocationDto>> GetLeaveAllocationsByUsername(string username);
    Task<IEnumerable<EmployeeLeaveAllocationDto>> GetMyLeavesAllocations(ClaimsPrincipal User);
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocations();
    Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsByLeaveType(string LeaveName);
    Task<GeneralServiceResponseDto> CreateLeaveAllocation(string username);
  }
}