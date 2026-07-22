namespace API.Controllers.Hr
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Dtos.Hr.JobTitles;
    using Application.Helpers;
    using Application.Interfaces;
    using Application.Interfaces.Auth;
    using Application.Services.Auth;
    using Domain.Constants.Enums;
    using Domain.Dtos.General;
    using Domain.Enties.hr;
    using Domain.Enties.Leaves;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    [ApiController]
    [Route("api/jobtitles")]
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
            var result = await userJobTitleService.GetJobTitlesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTitleById(int id)
        {
            var result = await _JobTitleService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("create")]
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
            var result = await _JobTitleService.SoftDeleteAsync(id);
            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetJobTitleByDepartment(int departmentId, Seniority? seniority)
        {
            var result = await userJobTitleService.GetJobTitleByDepartmentAndSeniorityAsync(departmentId, seniority);
            return Ok(result);
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

        [HttpGet("username")]
        public async Task<JobTitleDto> GetUserJobTitle(string userName)
        {
            var jobTitle = await userJobTitleService.GetJobTitleForUserAsync(userName);
            return (JobTitleDto)jobTitle;
        }

        [HttpPost("assign-job-title")]
        public async Task<IActionResult> AssignJobTitleToUser([FromBody] AssignJobTitleDto request)
        {
            var result = await userJobTitleService.AssignJobTitleToUserAsync(request); if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            return StatusCode(result.StatusCode, result.Message);
        }
    }
}