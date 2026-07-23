namespace Application.Mappings
{
    using Application.Dtos.Account.Logs;
    using Application.Dtos.Account.Messages;
    using Application.Dtos.Account.Users;
    using Application.Dtos.Hr.Clients;
    using Application.Dtos.Hr.Departments;
    using Application.Dtos.Hr.JobTitles;
    using Application.Dtos.Hr.Teams;
    using Application.Dtos.Leaves.LeaveAllocation;
    using Application.Dtos.Leaves.LeaveRequest;
    using Application.Dtos.Work.Projects;
    using Application.Dtos.Work.Timesheet;
    using AutoMapper;
    using Domain.Account;
    using Domain.Dtos.Shop;
    using Domain.Enties.hr;
    using Domain.Enties.Hr;
    using Domain.Enties.Leaves;
    using Domain.Enties.Work;

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
            CreateMap<Team, UserTeamListApplicableDto>().ReverseMap();
            CreateMap<Team, CreateUserTeamDto>().ReverseMap();
            CreateMap<TeamDto, CreateUserTeamDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, CreateUpdateClientDto>().ReverseMap();
            CreateMap<Project, CreateUpdateProjectDto>().ReverseMap();
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