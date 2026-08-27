namespace Application.Services.Leaves
{
    using Application.Dtos.Leaves.LeaveRequest;
    using Application.Helpers;
    using Application.Interfaces.Auth;
    using Application.Interfaces.Leaves;
    using AutoMapper;
    using Domain.Account;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;
    using Domain.Enties.Leaves;
    using Domain.Entities;
    using FluentResults;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly DataContext dataContext;
        private readonly ILogService logService;

        public LeaveRequestService(DataContext dataContext, ILogService logService)
        {
            this.dataContext = dataContext;
            this.logService = logService;
        }
        public async Task<IEnumerable<LeaveRequestDto>> GetAllLeaveRequestsAsync()
        {
            var leaveRequests = await (from request in dataContext.LeaveRequests
                                       join user in dataContext.Users on request.UserName equals user.UserName
                                       join leaveType in dataContext.LeaveTypes on request.LeaveTypeId equals leaveType.Id
                                       where !request.IsDeleted && request.Status == Status.Pending
                                       select new LeaveRequestDto
                                       {
                                           Id = request.Id,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           NumberOfDays = request.NumberOfDays,
                                           LeaveName = leaveType.Name,
                                           StartDate = request.StartDate,
                                           EndDate = request.EndDate,
                                           Status = request.Status,
                                           RequestedDate = request.DateRequested,
                                       }).ToListAsync();

            if (leaveRequests == null)
            {
                return (IEnumerable<LeaveRequestDto>)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "LeaveRequest Empty");
            }

            return leaveRequests;
        }

        public async Task<GeneralServiceResponseDto> CreateLeaveRequestAsync(ClaimsPrincipal user, CreateLeaveRequestDto createLeaveRequestDto)
        {
            if (createLeaveRequestDto.StartDate.Date <= DateTime.Today)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Leave cannot start before today");
            }

            int requestedDays = (int)(createLeaveRequestDto.EndDate.Date - createLeaveRequestDto.StartDate.Date).TotalDays + 1;

            if (requestedDays <= 0)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Invalid number of days requested");
            }

            var allocation = await dataContext.LeaveAllocations
                .Where(x => x.Employee.UserName == user.Identity.Name && x.LeaveTypeId == createLeaveRequestDto.LeaveTypeId)
                .FirstOrDefaultAsync();

            if (allocation == null)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Leave Type not allocated for the user");
            }

            if (requestedDays > allocation.NumberOfDays)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "You don't have enough days for the applied leave");
            }
            var overlappingLeave = await CheckForOverlappingLeaveRequestAsync(user, createLeaveRequestDto.StartDate, createLeaveRequestDto.EndDate);

            if (overlappingLeave != null)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Leave request overlaps with an existing leave request");
            }

            var newLeaveRequest = new LeaveRequest
            {
                LeaveTypeId = allocation.LeaveTypeId,
                StartDate = createLeaveRequestDto.StartDate,
                EndDate = createLeaveRequestDto.EndDate,
                NumberOfDays = requestedDays,
                UserName = user.Identity.Name,
                Status = Status.Pending,
                DateRequested = DateTime.Now,
                RequestComments = ""
            };

            dataContext.LeaveRequests.Add(newLeaveRequest);
            await dataContext.SaveChangesAsync();

            dataContext.LeaveAllocations.Update(allocation);
            await DeductLeaveDaysAsync(user?.Identity?.Name, createLeaveRequestDto.LeaveTypeId, requestedDays);
            await logService.SaveNewLogAsync(user.Identity.Name, "Leave Request");
            await dataContext.SaveChangesAsync();

            return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, "LeaveRequestCreatedSuccessfully"); // Return the created leave request DTO
        }

        public async Task<GeneralServiceResponseDto> DeleteLeaveRequestAsync(ClaimsPrincipal User, int leaveRequestId)
        {
            try
            {

                var leaveRequest = await dataContext.LeaveRequests
                    .FirstOrDefaultAsync(lr => lr.Id == leaveRequestId);

                if (leaveRequest == null)
                {
                    return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Leave request not found.");
                }

                dataContext.LeaveRequests.Remove(leaveRequest);
                await dataContext.SaveChangesAsync();
                await AddLeaveDaysAsync(leaveRequest.UserName, (int)leaveRequest.LeaveTypeId, leaveRequest.NumberOfDays);
                
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Delete Successfully");
            }
            catch (Exception ex)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, ex.Message);
            }
        }

        public async Task<LeaveRequestDto?> GetLeaveRequestsByIdAsync(int requestId)
        {
            var leaveRequests = await (from request in dataContext.LeaveRequests
                                       join user in dataContext.Users on request.UserName equals user.UserName
                                       join leaveType in dataContext.LeaveTypes on request.LeaveTypeId equals leaveType.Id
                                       where request.Id == requestId 
                                       select new LeaveRequestDto
                                       {
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           LeaveName = leaveType.Name,
                                           NumberOfDays = request.NumberOfDays,
                                           UserName = user.UserName ?? string.Empty,
                                           EndDate = request.EndDate,
                                           RequestedDate = request.StartDate,
                                           StartDate = request.StartDate,
                                           Status = request.Status
                                       }).FirstOrDefaultAsync();

            return leaveRequests;
        }

        public async Task<GeneralServiceResponseDto> ProcessLeaveRequestAsync(ClaimsPrincipal User, int leaveRequestId, Status status)
        {
            var leaveRequestEntity = await dataContext.LeaveRequests.FindAsync(leaveRequestId);

            if (leaveRequestEntity?.UserName == User?.Identity?.Name)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "You cannot process your own leave request");
            }
            if (leaveRequestEntity == null)
            {
                return ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "Leave request not found.");
            }
            if (leaveRequestEntity.IsDeleted == true)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "The leave request has been deleted and cannot be processed.");
            }

            leaveRequestEntity.Status = status;
            var leaveRequestDetails = await dataContext.LeaveRequests.Where(x => x.Id == leaveRequestId).FirstOrDefaultAsync();


            if (status == Status.Declined)
            {
                await AddLeaveDaysAsync(leaveRequestDetails.UserName, (int)leaveRequestDetails.LeaveTypeId, leaveRequestEntity.NumberOfDays);
            }
            else if (status == Status.Approved)
            {
                await DeductLeaveDaysAsync(leaveRequestDetails.UserName, (int)leaveRequestDetails.LeaveTypeId, leaveRequestEntity.NumberOfDays);
            }
            await dataContext.SaveChangesAsync();

            return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, "Leave Processed Succesasfully");
        }

        public async Task<GeneralServiceResponseDto> UpdateLeaveRequestAsync(ClaimsPrincipal user, int leaveRequestId, UpdateLeaveRequestDto updateLeaveRequestDto)
        {
            var leaveRequest = await dataContext.LeaveRequests.FindAsync(leaveRequestId);
            if (leaveRequest == null)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, "Leave request not found");
            }

            var result = await CheckAvailableDaysAsync(user, updateLeaveRequestDto.StartDate, updateLeaveRequestDto.EndDate);
            if (!result.IsSuccess)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, result.Errors.First().Message);
            }

            string specificUsername = user.FindFirstValue(ClaimTypes.Name);

            var overlappingLeave = await CheckForOverlappingLeaveRequestAsync(user, updateLeaveRequestDto.StartDate, updateLeaveRequestDto.EndDate);

            if (overlappingLeave != null)
            {
                return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, "Leave Overlaps With Existing Leave Application");
            }

            await AddLeaveDaysAsync(leaveRequest.UserName, (int)leaveRequest.LeaveTypeId, leaveRequest.NumberOfDays);
            leaveRequest.StartDate = updateLeaveRequestDto.StartDate;
            leaveRequest.EndDate = updateLeaveRequestDto.EndDate;
            leaveRequest.RequestComments = updateLeaveRequestDto.Comment;

            dataContext.LeaveRequests.Update(leaveRequest);
            await dataContext.SaveChangesAsync();

            return (GeneralServiceResponseDto)ResponseHelper.CreateResponse(true, 200, "Leave updated successfully");
        }
        private async Task<Result> CheckAvailableDaysAsync(ClaimsPrincipal user, DateTime startDate, DateTime endDate)
        {
            var allocation = await dataContext.LeaveAllocations
                .Where(x => x.Employee.UserName == user.Identity.Name)
                .FirstOrDefaultAsync();

            if (allocation == null)
            {
                return Result.Fail("Leave Type not allocated for the user");
            }

            int requestedDays = (int)(endDate - startDate).TotalDays;

            if (requestedDays > allocation.NumberOfDays)
            {
                return Result.Fail("You don't have enough days for the applied leave");
            }

            return Result.Ok();
        }
        private async Task<LeaveRequest> CheckForOverlappingLeaveRequestAsync(ClaimsPrincipal user, DateTime startDate, DateTime endDate)
        {
            var overlaps = await this.dataContext.LeaveRequests
                .Where(x => x.UserName.Equals(user.Identity.Name) &&
                            ((x.StartDate <= startDate && x.EndDate >= startDate) ||
                             (x.StartDate <= endDate && x.EndDate >= endDate)))
                .FirstOrDefaultAsync();
            return overlaps;
        }
        private async Task<Result> DeductLeaveDaysAsync(string username, int leaveTypeId, int days)
        {
            var allocation = await dataContext.LeaveAllocations
                .Where(x => x.Employee.UserName == username && x.LeaveType.Id == leaveTypeId)
                .FirstOrDefaultAsync();

            if (allocation == null)
            {
                return Result.Fail("Leave Type not allocated for the user");
            }

            allocation.NumberOfDays -= days;
            dataContext.LeaveAllocations.Update(allocation);
            await dataContext.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetLeaveRequestsByUserAsync(string username)
        {
            var employee = await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (employee == null)
            {
                return (IEnumerable<LeaveRequestDto>)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "User not found");
            }
            var leaveAllocations = await dataContext.LeaveRequests
                .Include(x => x.LeaveType) 
                .Where(x => x.UserName == username) 
                .Select(x => new LeaveRequestDto
                {
                    Id = x.Id,
                    NumberOfDays = x.NumberOfDays,
                    LeaveName = x.LeaveType.Name,
                    UserName = x.UserName,
                    EndDate = x.EndDate,
                    RequestedDate = x.DateRequested,
                    StartDate = x.StartDate,
                    Status = x.Status,
                })
                .ToListAsync();
            return leaveAllocations;
        }
        private async Task<Result<int>> AddLeaveDaysAsync(string username, int leaveTypeId, int days)
        {
            var allocation = await dataContext.LeaveAllocations
                .Where(x => x.Employee.UserName == username && x.LeaveType.Id == leaveTypeId)
                .FirstOrDefaultAsync();

            if (allocation == null)
            {
                return Result.Fail<int>("Leave Type not allocated for the user");
            }

            allocation.NumberOfDays += days; 
            dataContext.LeaveAllocations.Update(allocation);
            await dataContext.SaveChangesAsync();

            return Result.Ok(allocation.NumberOfDays);
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetUpComingLeavesAsync()
        {
            var leaveRequests = await (from request in dataContext.LeaveRequests
                                       join user in dataContext.Users on request.UserName equals user.UserName
                                       join leaveType in dataContext.LeaveTypes on request.LeaveTypeId equals leaveType.Id
                                       where !request.IsDeleted && request.StartDate > DateTime.Today
                                       select new LeaveRequestDto
                                       {
                                           Id = request.Id,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           NumberOfDays = request.NumberOfDays,
                                           LeaveName = leaveType.Name,
                                           StartDate = request.StartDate,
                                           EndDate = request.EndDate,
                                           Status = request.Status,
                                           RequestedDate = request.DateRequested,
                                       }).ToListAsync();

            if (leaveRequests == null)
            {
                return (IEnumerable<LeaveRequestDto>)ResponseHelper.CreateResponse(false, StatusCodes.Status404NotFound, "LeaveRequest Empty");
            }

            return leaveRequests;
        }
    }
}