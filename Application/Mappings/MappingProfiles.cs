namespace Application.Mappings
{
  using AutoMapper;
  using Domain.Account;
  using Domain.Dtos.Account;
  using Domain.Dtos.Departments;
  using Domain.Dtos.GeneralAdmin;
  using Domain.Dtos.JobTitles;
  using Domain.Dtos.LeaveAllocation;
  using Domain.Dtos.LeaveRequest;
  using Domain.Dtos.Timesheet;
  using Domain.Enties;
  using Domain.Enties.Leaves;
  using Domain.Enties.TimeSheets;
  using Domain.Entities;
  using Domain.Entities.TimeSheets;

  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
      CreateMap<Department, DepartmentDto>().ReverseMap();
      CreateMap<Department, UpdateDepartmentDto>().ReverseMap();
      CreateMap<Log, GetLogDto>().ReverseMap();
      CreateMap<Message, GetMessageDto>().ReverseMap();
      CreateMap<JobTitle, JobTitleDto>().ReverseMap();
      CreateMap<JobTitle, UpdateCreateJobTitleDto>().ReverseMap();
      CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
      CreateMap<LeaveRequestDto, MyLeaveRequestDto>().ReverseMap();
      CreateMap<LeaveAllocation, EmployeeLeaveAllocationDto>().ReverseMap();
      CreateMap<Team, TeamDto>().ReverseMap();
      CreateMap<Client, ClientDto>().ReverseMap();
      CreateMap<Project, CreateProjectDto>().ReverseMap();
      CreateMap<Project, ProjectDto>().ReverseMap();
      CreateMap<ApplicationUser, UserInfoResult>().ReverseMap();
      CreateMap<TimesheetEntry,TimesheetDetailDto>().ReverseMap();
      CreateMap<TimesheetEntry,TimesheetCreateModifyDto>().ReverseMap();
      CreateMap<TimesheetCreateModifyDto, TimesheetEntry>()
           .ForMember(dest => dest.TimesheetDate, opt => opt.MapFrom(src => src.TimesheetDate))
           .ForMember(dest => dest.Username, opt => opt.Ignore()).ReverseMap(); 
    }
  }
}