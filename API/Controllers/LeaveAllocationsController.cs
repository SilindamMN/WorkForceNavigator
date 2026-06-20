namespace API.Controllers.Maintanance
{
  using Application.Services;
  using Domain.Dtos.LeaveAllocation;
  using Domain.Enties.Leaves;
  using Microsoft.AspNetCore.Mvc;
  using Application.Interfaces;
  using Domain.Dtos.LeaveTypes;
  using Domain.Dtos.Account;
  using Microsoft.AspNetCore.Identity;

  [ApiController]
  [Route("api/[controller]")]
  public class LeaveAllocationController : ControllerBase
  {
    private readonly ILeaveAllocationService leaveAllocationService;

    public LeaveAllocationController(ILeaveAllocationService leaveAllocationService)
    {
      this.leaveAllocationService = leaveAllocationService;
    }

    [HttpGet]
    [Route("LeaveAllocationByUsereName")]
    public async Task<ActionResult<IEnumerable<EmployeeLeaveAllocationDto>>> GetUserAllocationByUserNamesync(string userName)
    {
      var allocations = await leaveAllocationService.GetLeaveAllocationsByUsername(userName);

      if (allocations == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }

      return Ok(allocations); // Return HTTP 200 OK with the allocations
    }

    [HttpGet]
    [Route("LeaveAllocationByLeaveName")]
    public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> GetUserAllocationByLeaveNamesync(string Leavename)
    {
      var allocations = await leaveAllocationService.GetLeaveAllocationsByLeaveType(Leavename);

      if (allocations == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }

      return Ok(allocations); // Return HTTP 200 OK with the allocations
    }

    [HttpGet]
    [Route("MyLeaveAllocations")]
    public async Task<ActionResult<IEnumerable<EmployeeLeaveAllocationDto>>> GetMyAllocationsync()
    {
      var allocations = await leaveAllocationService.GetMyLeavesAllocations(User);

      if (allocations == null)
      {
        return NotFound(); // Return HTTP 404 Not Found if user not found
      }
      return Ok(allocations); // Return HTTP 200 OK with the allocations
    }

    [HttpGet("LeaveAllocations")]
    public async Task<IActionResult> GetLeaveAllocations()
    {
      var result = await leaveAllocationService.GetLeaveAllocations();
      return Ok(result);
    }
  }
}