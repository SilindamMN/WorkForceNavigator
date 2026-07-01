namespace API.Controllers.GeneralAdmin
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Application.Interfaces.GenericInterfaces;
    using Domain.Dtos.GeneralAdmin;
    using Domain.Enties;
    using Domain.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IGenericService<Manager, ManagerDto> _managerService;
        private readonly IManageInterface _managerInterface;

        public ManagerController(
            IGenericService<Manager, ManagerDto> managerService,
            IManageInterface managerInterface)
        {
            _managerService = managerService;
            _managerInterface = managerInterface;
        }

        // GET: all managers (custom query with joins)
        [HttpGet]
        public async Task<IActionResult> GetAllManagers()
        {
            var result = await _managerInterface.GetAllManagers();
            return Ok(result);
        }

        // GET: manager by user id
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetManagerById(string userId)
        {
            var result = await _managerInterface.GetManagerByIdAsync(userId);

            if (result is null)
                return NotFound("Manager not found");

            return Ok(result);
        }

        // CREATE (Generic)
        [HttpPost]
        public async Task<IActionResult> CreateManager([FromBody] ManagerDto dto)
        {
            var result = await _managerService.CreateAsync(dto);

            if (result.IsSucceed)
                return Ok(result.Message);

            return StatusCode(result.StatusCode, result.Message);
        }

        // UPDATE (Generic)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] ManagerDto dto)
        {
            var result = await _managerService.UpdateAsync(id, dto);

            if (result.IsSucceed)
                return Ok(result.Message);

            return StatusCode(result.StatusCode, result.Message);
        }

        // DELETE (Soft delete Generic)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var result = await _managerService.SoftDelete(id);

            if (result.IsSucceed)
                return Ok(result.Message);

            return StatusCode(result.StatusCode, result.Message);
        }

        // UNDO DELETE
        [HttpDelete("{id}/undo")]
        public async Task<IActionResult> UndoDeleteManager(int id)
        {
            var result = await _managerService.UndoSoftDeleteAsync(id);

            if (result.IsSucceed)
                return Ok(result.Message);

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}