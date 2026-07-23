namespace API.Controllers.Leaves
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Application.Services.Auth;
    using Domain.Constants;
    using Application.Services;
    using Application.Interfaces.Auth;
    using AutoMapper;
    using Domain.Entities;
    using Application.Dtos.Leaves.LeaveAllocation;
    using Application.Dtos.Leaves.LeaveRequest;
    using Domain.Constants.Enums;
    using Application.Interfaces.Leaves;

    [ApiController]
  [Route("api/leave-requests")]
  public class LeaveRequestController : ControllerBase
  {
    private readonly ILeaveRequestService leaveRequestService;
    private readonly IMapper mapper;

    public LeaveRequestController(ILeaveRequestService leaveRequestService,IMapper mapper)
    {
      this.leaveRequestService = leaveRequestService;
      this.mapper = mapper;
    }

        [HttpGet]
        [Route("up-coming")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetLeaveRequests()
        {
            var logs = await leaveRequestService.GetUpComingLeavesAsync();
            return Ok(logs);
        }
        [HttpPost]
    [Authorize]
    [Route("create")]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestDto createLeave)
    {
      var result = await leaveRequestService.CreateLeaveRequestAsync(User,createLeave);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpGet]
    [Route("by-username")]
    public async Task<ActionResult<IEnumerable<EmployeeLeaveAllocationDto>>> GetUserAllocationByUserNamesync(string userName)
    {
      var leaveRequests = await leaveRequestService.GetLeaveRequestsByUserAsync(userName);

      if (leaveRequests == null)
      {
        return NotFound(); 
      }

      return Ok(leaveRequests); 
    }

    [HttpGet]
    [Route("mine")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetMyLeaveRequests()
    {
      var leaveRequests = await leaveRequestService.GetLeaveRequestsByUserAsync(User.Identity.Name);
      var mappr = mapper.Map<IEnumerable<MyLeaveRequestDto>>(leaveRequests);

      if (mappr == null)
      {
        return NotFound(); 
      }

      return Ok(mappr);
    }

    [HttpGet]
    [Route("{requestId}")]
    public async Task<ActionResult<LeaveRequestDto>> GetLeaveRequestById([FromRoute] int requestId)
    {
      var leaveRequests = await leaveRequestService.GetLeaveRequestsByIdAsync(requestId);
      if (leaveRequests is null)
      {
        return NotFound("leaveRequestId not found");
      }
      else
      {
        return Ok(leaveRequests);
      }
    }

    [HttpPatch]
    [Route("{id}")]
    [Authorize(Roles = StaticUserRoles.ADMIN)]
    public async Task<IActionResult> UpdateLeaveRequest(int leaveRequestId, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
    {
      var updateLeaveRequest = await leaveRequestService.UpdateLeaveRequestAsync(User, leaveRequestId, updateLeaveRequestDto);
      if (updateLeaveRequest.IsSucceed)
      {
        return Ok(updateLeaveRequest.Message);
      }
      else
      {
        return StatusCode(updateLeaveRequest.StatusCode, updateLeaveRequest.Message);
      }
    }

    [HttpPost]
    [Route("process")]
    [Authorize(Roles = StaticUserRoles.ADMIN)]
    public async Task<IActionResult> ProcessLeaveRequest(int leaveRequestId, Status status)
    {
      var processLeaveRequest = await leaveRequestService.ProcessLeaveRequestAsync(User, leaveRequestId, status);
      if (processLeaveRequest.IsSucceed)
      {
        return Ok(processLeaveRequest.Message);
      }
      else
      {
        return StatusCode(processLeaveRequest.StatusCode, processLeaveRequest.Message);
      }
    }

    [HttpGet("upcoming")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetUpcomingLeaves()
    {
      try
      {
        var upcomingLeaves = await leaveRequestService.GetUpComingLeavesAsync();
        if (upcomingLeaves == null || !upcomingLeaves.Any())
        {
          return NotFound("No upcoming leaves found.");
        }
        return Ok(upcomingLeaves);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }

    [HttpPost]
    [Route("delete")]
    [Authorize(Roles = StaticUserRoles.ADMIN)]
    public async Task<IActionResult> DeleteLeaveRequest(int leaveRequestId)
    {
      var deleteLeaveRequest = await leaveRequestService.DeleteLeaveRequestAsync(User, leaveRequestId);
      if (deleteLeaveRequest.IsSucceed)
      {
        return Ok(deleteLeaveRequest.Message);
      }
      else
      {
        return StatusCode(deleteLeaveRequest.StatusCode, deleteLeaveRequest.Message);
      }
    }
  }
}