namespace Application.Services
{
  using Application.Helpers;
  using Application.Interfaces;
  using Application.Interfaces.Auth;
  using AutoMapper;
  using Domain.Dtos;
  using Domain.Dtos.General;
  using Domain.Dtos.LeaveAllocation;
  using Domain.Dtos.LeaveRequest;
  using Domain.Enties;
  using Domain.Enties.Leaves;
  using Domain.Enums;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.EntityFrameworkCore;
  using Persistence;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;
  using System.Threading.Tasks;

  public class LeaveAllocationService : ILeaveAllocationService
  {
    private readonly DataContext dataContext;
    private readonly ILogService logService;
    private readonly IMapper mapper;

    public LeaveAllocationService(DataContext dataContext,ILogService logService,IMapper mapper)
    {
      this.dataContext = dataContext;
      this.logService = logService;
      this.mapper = mapper;
    }
    public async Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocations()
    {
      var leaveAllocations = await (from la in dataContext.LeaveAllocations
                                    join u in dataContext.Users on la.Username equals u.UserName
                                    select new LeaveAllocationDto
                                    {
                                       Username = u.UserName,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      NumberOfDays = la.NumberOfDays,
                                      LeaveName= la.LeaveType.Name
                                    }).ToListAsync();

      return leaveAllocations;
    }

    public async Task<GeneralServiceResponseDto> CreateLeaveAllocation(string username)
    {
      // Retrieve the user based on the username
      var user = await dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

      if (user == null)
      {
        return ResponseHelper.CreateResponse(false, 400, "User not found");
      }

      // Retrieve all leave types
      var leaveTypes = await dataContext.LeaveTypes.ToListAsync();

      // Create leave allocations for each leave type for the user
      var newAllocations = leaveTypes.Select(leaveType => new LeaveAllocation
      {
        Username =username, // Assuming there's an EmployeeId property in the User entity
        LeaveTypeId = leaveType.Id,
        NumberOfDays = leaveType.DefaultDays, // You may adjust this based on your leave type properties
                                              // ... other properties
      }).ToList();

      dataContext.LeaveAllocations.AddRange(newAllocations);
      await logService.SaveNewLog(username, "Leaves Allocated");
      await dataContext.SaveChangesAsync();

       return ResponseHelper.CreateResponse(true, 200, "LeaveAllocated Successfully");
    }

    public async Task<IEnumerable<EmployeeLeaveAllocationDto>> GetLeaveAllocationsByUsername(string username)
    {
      var employee = await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
      if (employee == null)
      {
        return (IEnumerable<EmployeeLeaveAllocationDto>)ResponseHelper.CreateResponse(false, 400, "User not found");
      }
      var leaveAllocations = await dataContext.LeaveAllocations
          .Include(x => x.LeaveType) // Include related LeaveType entity
          .Where(x => x.Username == username) // Filter by username
          .Select(x => new EmployeeLeaveAllocationDto
          {
            NumberOfDays = x.NumberOfDays,
            LeaveName = x.LeaveType.Name,
          })
          .ToListAsync();
      return mapper.Map<IEnumerable<EmployeeLeaveAllocationDto>>(leaveAllocations);
    }

    public async Task<IEnumerable<EmployeeLeaveAllocationDto>> GetMyLeavesAllocations(ClaimsPrincipal User)
    {
      var employee = await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
      if (employee == null)
      {
        return (IEnumerable<EmployeeLeaveAllocationDto>)ResponseHelper.CreateResponse(false, 400, "User not found");
      }
      var leaveAllocations = await dataContext.LeaveAllocations
          .Include(x => x.LeaveType) // Include related LeaveType entity
          .Where(x => x.Username == User.Identity.Name) // Filter by username
          .Select(x => new EmployeeLeaveAllocationDto
          {
            Id = x.Id,
            NumberOfDays = x.NumberOfDays,
            LeaveName = x.LeaveType.Name,
          })
          .ToListAsync();

      return mapper.Map<IEnumerable<EmployeeLeaveAllocationDto>>(leaveAllocations);
    }

    public async Task<IEnumerable<LeaveAllocationDto>> GetLeaveAllocationsByLeaveType(string LeaveName)
    {
      var allocation = await dataContext.LeaveAllocations
          .Where(z => z.LeaveType.Name == LeaveName)
          .FirstOrDefaultAsync();

      if (allocation is null)
      {
        return (IEnumerable<LeaveAllocationDto>)ResponseHelper.CreateResponse(false, 400, "No Leave Allocations for this ");
      }

      var leaveAllocations = await (from la in dataContext.LeaveAllocations
                                    join u in dataContext.LeaveTypes on la.LeaveTypeId equals u.Id
                                    join user in dataContext.Users on la.Username equals user.UserName
                                    where (la.LeaveType.Name).Equals(LeaveName)
                                    select new LeaveAllocationDto
                                    {
                                      Username = user.UserName,
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      NumberOfDays = la.NumberOfDays,
                                      LeaveName = la.LeaveType.Name
                                    }).ToListAsync();
      return leaveAllocations;
    }
  }
}