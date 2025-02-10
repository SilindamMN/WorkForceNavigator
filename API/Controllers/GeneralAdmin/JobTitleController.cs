
namespace API.Controllers.GeneralAdmin
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Application.Helpers;
  using Application.Interfaces;
  using Application.Interfaces.GenericInterfaces;
  using Application.Services.Auth;
  using Application.Services.GenericServices;
  using Domain.Dtos.Account;
  using Domain.Dtos.General;
  using Domain.Dtos.JobTitles;
  using Domain.Enties;
  using Domain.Enties.Leaves;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Persistence;

  [ApiController]
  [Route("api/[controller]")]
  public class JobTitleController : ControllerBase
  {
    private readonly IGenericService<JobTitle, JobTitleDto> _JobTitleService;
    private readonly IGenericService<JobTitle, UpdateCreateJobTitleDto> _JobTitleUpdateService;
    private readonly IUserJobTitleService userJobTitleService;

    public JobTitleController(
        IGenericService<JobTitle, JobTitleDto> JobTitleService,
        IGenericService<JobTitle, UpdateCreateJobTitleDto> JobTitleUpdateService,
        IUserJobTitleService userJobTitleService)
    {
      _JobTitleService = JobTitleService;
      _JobTitleUpdateService = JobTitleUpdateService;
      this.userJobTitleService = userJobTitleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobTitles()
    {
      var result = await userJobTitleService.GetJobTitles();
      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobTitleById(int id)
    {
      var result = await _JobTitleService.GetByIdAsync(id);
      return Ok(result);
    }

    [HttpPost("CreateJobTitle")]
    public async Task<IActionResult> CreateJobTitle([FromBody] UpdateCreateJobTitleDto JobTitleDto)
    {
      var result = await _JobTitleUpdateService.CreateAsync(JobTitleDto);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobTitle(int id, [FromBody] UpdateCreateJobTitleDto updateJobTitleDto)
    {
      var result = await _JobTitleUpdateService.UpdateAsync(id, updateJobTitleDto);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      else
      {
        return StatusCode(result.StatusCode, result.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteJobTitle(int id)
    {
      var result = await _JobTitleService.SoftDelete(id);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpDelete("{id}/undo")]
    public async Task<IActionResult> UnSoftDeleteJobTitle(int id)
    {
      var result = await _JobTitleService.UndoSoftDeleteAsync(id);
      if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }

    [HttpGet("JobTitleUse")]
    public async Task<JobTitleDto> GetUserJobTitle(string userName)
    {
      var jobTitle = await userJobTitleService.GetJobTitleForUser(userName);
      return (JobTitleDto)jobTitle;
    }
 
    [HttpPost("AssignJobTitle")]
    public async Task<IActionResult> AssignJobTitleToUser([FromBody] AssignJobTitleDto request)
    {
      var result = await userJobTitleService.AssignJobTitleToUser(request); if (result.IsSucceed)
      {
        return Ok(result.Message);
      }
      return StatusCode(result.StatusCode, result.Message);
    }
  }
}