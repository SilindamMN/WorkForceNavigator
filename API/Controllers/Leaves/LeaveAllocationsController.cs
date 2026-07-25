namespace API.Controllers.Leaves
{
    using Application.Services;
    using Domain.Enties.Leaves;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Application.Dtos.Leaves.LeaveAllocation;
    using Application.Interfaces.Leaves;
    using Microsoft.AspNetCore.Authorization;
    using Domain.Constants;

    [ApiController]
    [Route("api/leave-allocations")]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly ILeaveAllocationService leaveAllocationService;

        public LeaveAllocationController(ILeaveAllocationService leaveAllocationService)
        {
            this.leaveAllocationService = leaveAllocationService;
        }

        [HttpGet]
        [Route("username")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<ActionResult<IEnumerable<EmployeeLeaveAllocationDto>>> GetUserAllocationByUserNamesync(string userName)
        {
            var allocations = await leaveAllocationService.GetLeaveAllocationsByUsernameAsync(userName);

            if (allocations == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }

            return Ok(allocations); // Return HTTP 200 OK with the allocations
        }

        [HttpGet]
        [Route("leave-name")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> GetUserAllocationByLeaveNamesync(string leavename)
        {
            var allocations = await leaveAllocationService.GetLeaveAllocationsByLeaveTypeAsync(leavename);

            if (allocations == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }

            return Ok(allocations); // Return HTTP 200 OK with the allocations
        }

        [HttpGet]
        [Route("mine")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<ActionResult<IEnumerable<EmployeeLeaveAllocationDto>>> GetMyAllocationsync()
        {
            var allocations = await leaveAllocationService.GetMyLeavesAllocationsAsync(User);

            if (allocations == null)
            {
                return NotFound(); // Return HTTP 404 Not Found if user not found
            }
            return Ok(allocations); // Return HTTP 200 OK with the allocations
        }

        [HttpGet("all")]
        [Authorize(Roles = StaticUserRoles.USER)]
        public async Task<IActionResult> GetLeaveAllocations()
        {
            var result = await leaveAllocationService.GetLeaveAllocationsAsync();
            return Ok(result);
        }
    }
}